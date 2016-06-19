using System.Collections.Generic;
using System.Linq;
using Alfred.Dal.Entities.Member;
using Alfred.Dal.Interfaces;
using Alfred.Model;
using Alfred.Model.Members;
using Alfred.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Tests.Services
{
    [TestFixture]
    public class MemberServiceTests
    {
        private Fixture _fixture;

        [SetUp]
        public void Initialize()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void Should_return_5_members_when_service_get_all_members()
        {
            var members = _fixture.Build<Member>()
                .Without(x => x.Artifacts)
                .CreateMany(5);

            var fakeModelFactory = Substitute.For<IModelFactory>();
            fakeModelFactory.CreateMemberModel(Arg.Any<Member>()).Returns(GetMemberModel(members.FirstOrDefault()));
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMembers().ReturnsForAnyArgs(members);

            var memberService = new MemberService(fakeRepo, fakeModelFactory);
            var result = memberService.GetMembers();
            result.FirstOrDefault().Should().BeOfType<MemberModel>();
            result.Count().Should().Be(members.Count());
        }

        [Test]
        public void Should_return_member_2_when_service_gets_member_with_id_2()
        {
            var memberToSearch = _fixture.Build<Member>()
                .With(x => x.Id, 2)
                .Without(x => x.Artifacts)
                .Create();

            var fakeModelFactory = Substitute.For<IModelFactory>();
            fakeModelFactory.CreateMemberModel(Arg.Is<Member>(x => x.Id == 2))
                .Returns(GetMemberModel(memberToSearch));
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMember(Arg.Is(2)).Returns(memberToSearch);

            var memberService = new MemberService(fakeRepo, fakeModelFactory);

            var result = memberService.GetMember(2);
            result.Should().BeOfType<MemberModel>();
            result.Email.Should().Be(memberToSearch.Email);
            result.FirstName.Should().Be(memberToSearch.FirstName);
            result.LastName.Should().Be(memberToSearch.LastName);
        }

        [Test]
        public void Should_return_null_when_service_dont_find_member_with_id_2()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMember(Arg.Is(2)).ReturnsNull();

            var fakeModelFactory = Substitute.For<IModelFactory>();
            var memberService = new MemberService(fakeRepo, fakeModelFactory);
            var result = memberService.GetMember(2);
            result.Should().BeNull();
        }

        [Test]
        public void Should_create_member_when_member_has_valid_data()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var createMemberModel = _fixture.Build<CreateMemberModel>().Create();
            var member = GetMember(createMemberModel);
            fakeModelFactory.CreateMember(Arg.Any<CreateMemberModel>()).Returns(member);
            fakeRepo.GetMember(Arg.Is<string>(x => x == createMemberModel.Email)).ReturnsNull();
            var memberService = new MemberService(fakeRepo, fakeModelFactory);

            memberService.CreateMember(createMemberModel);
            fakeModelFactory.Received(1).CreateMember(Arg.Is<CreateMemberModel>(x => x.Email == createMemberModel.Email));
            fakeRepo.Received(1).GetMember(Arg.Is<string>(x => x == member.Email));
            fakeRepo.Received(1).SaveMember(Arg.Is<Member>(x => x.Email == member.Email));
        }

        [Test]
        public void Should_not_create_member_when_member_email_already_used()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var createMemberModel = _fixture.Build<CreateMemberModel>().Create();
            var member = GetMember(createMemberModel);
            fakeModelFactory.CreateMember(Arg.Any<CreateMemberModel>()).Returns(member);
            fakeRepo.GetMember(Arg.Is<string>(x => x == createMemberModel.Email)).Returns(member);
            var memberService = new MemberService(fakeRepo, fakeModelFactory);

            var result = memberService.CreateMember(createMemberModel);
            fakeModelFactory.Received(1).CreateMember(Arg.Is<CreateMemberModel>(x => x.Email == createMemberModel.Email));
            fakeModelFactory.DidNotReceive().CreateMemberModel(Arg.Is<Member>(x => x.Email == createMemberModel.Email));
            fakeRepo.Received(1).GetMember(Arg.Is<string>(x => x == createMemberModel.Email));
            fakeRepo.DidNotReceive().SaveMember(Arg.Is<Member>(x => x.Email == createMemberModel.Email));
            result.Should().Be(-1);
        }

        [Test]
        public void Should_update_member_when_member_has_valid_data()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var updateMemberModel = _fixture.Build<UpdateMemberModel>().Create();
            var member = _fixture.Create<Member>();
            fakeRepo.GetMember(Arg.Is(updateMemberModel.Id)).Returns(member);

            fakeModelFactory.CreateMember(Arg.Any<UpdateMemberModel>(), member).Returns(GetMember(updateMemberModel));
            var memberService = new MemberService(fakeRepo, fakeModelFactory);

            memberService.UpdateMember(updateMemberModel);
            fakeModelFactory.Received(1).CreateMember(Arg.Is<UpdateMemberModel>(x => x.Id == updateMemberModel.Id), member);
            fakeModelFactory.Received(1).CreateMemberModel(Arg.Is<Member>(x => x.Id == updateMemberModel.Id));
            fakeRepo.Received(1).GetMember(Arg.Is(updateMemberModel.Id));
            fakeRepo.Received(1).UpdateMember(Arg.Is<Member>(x => x.Id == updateMemberModel.Id));
        }

        [Test]
        public void Should_not_update_member_when_member_email_does_not_exist()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var updateMemberModel = _fixture.Build<UpdateMemberModel>().Create();
            fakeRepo.GetMember(Arg.Is(updateMemberModel.Id)).ReturnsNull();

            var memberService = new MemberService(fakeRepo, fakeModelFactory);
            memberService.UpdateMember(updateMemberModel);

            fakeModelFactory.DidNotReceive().CreateMember(Arg.Is<UpdateMemberModel>(x => x.Email == updateMemberModel.Email), null);
            fakeRepo.Received(1).GetMember(Arg.Is(updateMemberModel.Id));
            fakeRepo.DidNotReceive().UpdateMember(Arg.Is<Member>(x => x.Email == updateMemberModel.Email));
        }

        [Test]
        public void Should_delete_member_when_member_id_exist()
        {
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var fakeRepo = Substitute.For<IMemberRepository>();
            var member = _fixture.Build<Member>()
                .Without(x=>x.Artifacts)
                .With(x => x.Id, 2)
                .Create();

            fakeRepo.GetMember(Arg.Is<int>(x => x == 2)).Returns(member);
            var memberService = new MemberService(fakeRepo, fakeModelFactory);

            var result = memberService.DeleteMember(2);
            fakeRepo.Received(1).DeleteMember(Arg.Is<int>(x => x == 2));
            result.Should().BeTrue();
        }

        [Test]
        public void Should_not_delete_member_when_member_id_does_not_exist()
        {
            var fakeModelFactory = Substitute.For<IModelFactory>();
            var fakeRepo = Substitute.For<IMemberRepository>();            

            fakeRepo.GetMember(Arg.Is<int>(x => x == -1)).ReturnsNull();
            var memberService = new MemberService(fakeRepo, fakeModelFactory);

            var result = memberService.DeleteMember(-1);
            fakeRepo.DidNotReceive().DeleteMember(Arg.Is<int>(x => x == -1));
            result.Should().BeFalse();
        }

        private Member GetMember(CreateMemberModel createMemberModel)
        {
            return new Member
            {
                Email = createMemberModel.Email
            };
        }

        private Member GetMember(UpdateMemberModel createMemberModel)
        {
            return new Member
            {
                Id = createMemberModel.Id
            };
        }

        private MemberModel GetMemberModel(Member memberToSearch)
        {
            return new MemberModel
            {
                Email = memberToSearch.Email,
                FirstName = memberToSearch.FirstName,
                LastName = memberToSearch.LastName
            };
        }
    }
}

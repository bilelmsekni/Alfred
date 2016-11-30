using System.Linq;
using System.Threading.Tasks;
using Alfred.Domain.Repositories;
using Alfred.Domain.Services;
using Alfred.Models.Members;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Ploeh.AutoFixture;

namespace Alfred.Domain.Tests.Services
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
            var response = _fixture.Build<MemberResponseModel>()
                .With(x=>x.Results, _fixture.Build<MemberModel>()
                .CreateMany(5).ToList())
                .Create();

            var fakeCriteria = new MemberCriteriaModel();
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMembers(fakeCriteria).ReturnsForAnyArgs(response);

            var memberService = new MemberService(fakeRepo);
            var result = memberService.GetMembers(fakeCriteria).Result;
            result.Results.FirstOrDefault().Should().BeOfType<MemberModel>();
            result.Results.Count().Should().Be(5);
        }

        [Test]
        public void Should_return_member_2_when_service_gets_member_with_id_2()
        {
            var memberToSearch = _fixture.Build<MemberModel>()
                .With(x => x.Id, 2)
                .Create();

            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMember(Arg.Is(2)).Returns(memberToSearch);

            var memberService = new MemberService(fakeRepo);

            var result = memberService.GetMember(2).Result;
            result.Should().BeOfType<MemberModel>();
            result.Email.Should().Be(memberToSearch.Email);
            result.FirstName.Should().Be(memberToSearch.FirstName);
            result.LastName.Should().Be(memberToSearch.LastName);
        }

        [Test]
        public void Should_return_null_when_service_dont_find_member_with_id_2()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMember(Arg.Is(2)).Returns(Task.FromResult<MemberModel>(null));

            var memberService = new MemberService(fakeRepo);
            var result = memberService.GetMember(2).Result;
            result.Should().BeNull();
        }

        [Test]
        public void Should_create_member_when_member_has_valid_data()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var createMemberModel = _fixture.Build<CreateMemberModel>().Create();
            var member = GetMember(createMemberModel);
            fakeRepo.GetMember(Arg.Is<string>(x => x == createMemberModel.Email)).ReturnsNull();
            var memberService = new MemberService(fakeRepo);

            memberService.CreateMember(createMemberModel).ConfigureAwait(false);
            fakeRepo.Received(1).SaveMember(Arg.Is<CreateMemberModel>(x => x.Email == member.Email));
        }

        [Test]
        public void Should_not_create_member_when_member_is_null()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            
            var memberService = new MemberService(fakeRepo);

            var result = memberService.CreateMember(null).Result;
            fakeRepo.DidNotReceive().SaveMember(Arg.Any<CreateMemberModel>());
            result.Should().Be(-1);
        }

        [Test]
        public void Should_update_member_when_member_has_valid_data()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var updateMemberModel = _fixture.Build<UpdateMemberModel>().Create();

            var memberService = new MemberService(fakeRepo);

            memberService.UpdateMember(updateMemberModel).ConfigureAwait(false);
            fakeRepo.Received(1).UpdateMember(Arg.Is<UpdateMemberModel>(x => x.Id == updateMemberModel.Id));
        }

        [Test]
        public void Should_delete_member_when_member_id_exist()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();
            var member = _fixture.Build<MemberModel>()
                .With(x => x.Id, 2)
                .Create();

            fakeRepo.GetMember(Arg.Is<int>(x => x == 2)).Returns(member);
            var memberService = new MemberService(fakeRepo);

            var result = memberService.DeleteMember(2).Result;
            fakeRepo.Received(1).DeleteMember(Arg.Is<int>(x => x == 2));
            result.Should().BeTrue();
        }

        [Test]
        public void Should_not_delete_member_when_member_id_does_not_exist()
        {
            var fakeRepo = Substitute.For<IMemberRepository>();

            fakeRepo.GetMember(Arg.Is<int>(x => x == -1)).Returns(Task.FromResult<MemberModel>(null));
            var memberService = new MemberService(fakeRepo);

            var result = memberService.DeleteMember(-1).Result;
            fakeRepo.DidNotReceive().DeleteMember(Arg.Is<int>(x => x == -1));
            result.Should().BeFalse();
        }

        private MemberModel GetMember(CreateMemberModel createMemberModel)
        {
            return new MemberModel
            {
                Email = createMemberModel.Email
            };
        }
    }
}

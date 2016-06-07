using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alfred.Dal.Entities;
using Alfred.Dal.Interfaces;
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
                .Without(x => x.Communities)
                .Without(x => x.Artifacts)
                .CreateMany(5).AsEnumerable();
            
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMembers().ReturnsForAnyArgs(members);

            var memberService = new MemberService(fakeRepo);
            var result = memberService.GetMembers();
            //result.Should().BeOfType<IEnumerable<Member>>();
            result.Count().Should().Be(members.Count());
        }

        [Test]
        public void Should_return_member_2_when_service_gets_member_with_id_2()
        {
            var members = _fixture.Build<Member>()                
                .Without(x => x.Communities)
                .Without(x => x.Artifacts)
                .CreateMany(4);

            var memberToSearch = _fixture.Build<Member>()
                .Without(x => x.Communities)
                .Without(x => x.Artifacts)
                .With(x=>x.Id, 2)
                .Create();
            members.ToList().Add(memberToSearch);

            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMember(Arg.Is(2)).Returns(memberToSearch);

            var memberService = new MemberService(fakeRepo);
            var result = memberService.GetMember(2);
            result.Should().BeOfType<Member>();
            result.Id.Should().Be(2);
        }

        [Test]
        public void Should_return_null_when_service_dont_find_member_with_id_2()
        {            
            var fakeRepo = Substitute.For<IMemberRepository>();
            fakeRepo.GetMember(Arg.Is(2)).ReturnsNull();

            var memberService = new MemberService(fakeRepo);
            var result = memberService.GetMember(2);            
            result.Should().BeNull();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Auto = AutoMapper;

namespace Demo.AutoMapper
{
    [TestFixture]
    public class given_concrete_type_when_mapping_to_concrete_type_with_list_of_interfaces
    {
        private OuterOne _outerOne;

        [TestFixtureSetUp]
        public void Init()
        {
            Auto.Mapper.CreateMap<OuterTwo, OuterOne>()
                .ForMember(dest => dest.PropertyFive, opt => opt.ResolveUsing<PropertyFiveResolver>());
            Auto.Mapper.CreateMap<InnerTwo, InnerOne>();
        }

        public class PropertyFiveResolver : Auto.ValueResolver<OuterTwo, IEnumerable<IInner>>
        {
            protected override IEnumerable<IInner> ResolveCore(OuterTwo source)
            {
                return source.PropertyFive.Select(Auto.Mapper.Map<InnerOne>);
            }
        }

        [SetUp]
        public void SetUp()
        {
            var outerTwo = new OuterTwo
            {
                PropertyOne = "PropertyOne",
                PropertyTwo = "PropertyTwo",
                PropertyFive =
                    new[]
                    {
                        new InnerTwo {PropertyFour = "PropertyFour", PropertyThree = "PropertyThree"},
                        new InnerTwo {PropertyFour = "PropertyFour2", PropertyThree = "PropertyThree2"}
                    }
            };

            _outerOne = Auto.Mapper.Map<OuterOne>(outerTwo);
        }

        [Test]
        public void should_()
        {
            Auto.Mapper.AssertConfigurationIsValid();

            _outerOne.PropertyOne.Should().Be("PropertyOne");
            _outerOne.PropertyTwo.Should().Be("PropertyTwo");
            _outerOne.PropertyFive.ShouldAllBeEquivalentTo(new[]
            {
                new InnerOne {PropertyFour = "PropertyFour", PropertyThree = "PropertyThree"},
                new InnerOne {PropertyFour = "PropertyFour2", PropertyThree = "PropertyThree2"}
            });
        }



    }
}

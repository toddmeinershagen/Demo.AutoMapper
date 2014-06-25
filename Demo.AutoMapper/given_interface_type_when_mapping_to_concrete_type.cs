using FluentAssertions;
using NUnit.Framework;
using Auto = AutoMapper;

namespace Demo.AutoMapper
{
    [TestFixture]
    public class given_interface_type_when_mapping_to_concrete_type
    {
        private OuterTwo _outerTwo;

        [SetUp]
        public void SetUp()
        {
            Auto.Mapper.CreateMap<IOuter, OuterTwo>();
                //.ForMember(dest => dest.PropertyFive, opt => opt.ResolveUsing<PropertyFiveResolver>());
            Auto.Mapper.CreateMap<IInner, InnerTwo>();

            IOuter outerOne = new OuterOne
            {
                PropertyOne = "PropertyOne",
                PropertyTwo = "PropertyTwo",
                PropertyFive = new IInner[]
                {
                    new InnerOne {PropertyFour = "PropertyFour", PropertyThree = "PropertyThree"},
                    new InnerOne {PropertyFour = "PropertyFour2", PropertyThree = "PropertyThree2"}
                }
            };

            _outerTwo = Auto.Mapper.Map<OuterTwo>(outerOne);
        }

        //public class PropertyFiveResolver : Auto.ValueResolver<IOuter, IEnumerable<InnerTwo>>
        //{
        //    protected override IEnumerable<InnerTwo> ResolveCore(IOuter source)
        //    {
        //        return source.PropertyFive.Select(Auto.Mapper.Map<InnerTwo>);
        //    }
        //}
        
        [Test]
        public void should()
        {
            Auto.Mapper.AssertConfigurationIsValid();

            _outerTwo.PropertyOne.Should().Be("PropertyOne");
            _outerTwo.PropertyTwo.Should().Be("PropertyTwo");
            _outerTwo.PropertyFive.ShouldAllBeEquivalentTo(new[]
            {
                new InnerOne {PropertyFour = "PropertyFour", PropertyThree = "PropertyThree"},
                new InnerOne {PropertyFour = "PropertyFour2", PropertyThree = "PropertyThree2"}
            });
        }
    }
}

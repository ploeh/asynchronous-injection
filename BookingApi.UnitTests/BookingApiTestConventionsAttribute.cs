﻿using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Ploeh.Samples.BookingApi.UnitTests
{
    public class BookingApiTestConventionsAttribute : AutoDataAttribute
    {
        public BookingApiTestConventionsAttribute() :
            base(() => new Fixture().Customize(new AutoMoqCustomization()))
        {
        }
    }
}
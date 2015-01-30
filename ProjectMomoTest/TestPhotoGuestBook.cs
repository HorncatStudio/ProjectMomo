using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectMomo.Model;
using ProjectMomo.Helpers;
using System.Collections.Generic;

namespace ProjectMomoTest
{
    [TestClass]
    public class TestPhotoGuestBook
    {
        [TestMethod]
        public void ShallLoadFetchedPicture()
        {
          Guest guest = new Guest();
          List<Guest> guestList = new List<Guest>();
          guestList.Add(guest);

          PhotoGuestBook guestBook = new PhotoGuestBook();
          guestBook.Guests = guestList;
          guestBook.SetCurrentGuest( guest );

          FakeFetchPictureService fetchService = new FakeFetchPictureService();
          fetchService.RegisterListener(guestBook);

          ShowerPicture expectedPicture = new ShowerPicture(1);
          fetchService.SendPicture(expectedPicture);

          ShowerPicture actualPicture = guest.GetLastGuestBookPicture();

          Assert.AreSame( expectedPicture, actualPicture );
        }
    }
}

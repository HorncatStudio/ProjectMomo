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
          guestBook.loadGuests( guestList );
          guestBook.setCurrentGuest( guest );

          FakeFetchPictureService fetchService = new FakeFetchPictureService();
          fetchService.registerListener(guestBook);

          ShowerPicture expectedPicture = new ShowerPicture(1);
          fetchService.sendPicture(expectedPicture);

          ShowerPicture actualPicture = guest.getLastGuestBookPicture();

          Assert.AreSame( expectedPicture, actualPicture );
        }
    }
}

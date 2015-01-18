﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ProjectMomo.Model
{
  public class ShowerPicture
  {
    private const int InvalidId = -1;
    private int _Id;
    private bool _IsLoaded;
    private BitmapImage _Image;

    public string RelativePath { get; set; }

    public ShowerPicture()
      : this(InvalidId)
    {
    }

    public ShowerPicture( int id )
    {
      _Id = id;
      _IsLoaded = false;
      _Image = new BitmapImage();
    }

    public bool isNull()
    {
      return (InvalidId == _Id);
    }

    public bool isLoaded()
    {
      return _IsLoaded;
    }
  }
}

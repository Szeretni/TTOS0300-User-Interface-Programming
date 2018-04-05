using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birds
{
    class BirdViewModel
    {
        public static List<Bird> Get3TestBirds()
        {
            List<Bird> birds = new List<Bird>();
            birds.Add(new Bird { Name = "Angry", ImgFile = "BirdRed.PNG" ,Value=1000});
            birds.Add(new Bird { Name = "Nasty", ImgFile = "BirdBlack.PNG", Value = 500 });
            birds.Add(new Bird { Name = "Curious", ImgFile = "BirdYellow.PNG", Value = 250 });
            return birds;
        }
    }
}

using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Player
    {
        public int id = 0;
        public ArrayList birds = new ArrayList();

        public void SetBirds(string birdList, string seperator = ", ")
        {
            string[] elements = birdList.Split(new string[] {seperator}, System.StringSplitOptions.RemoveEmptyEntries);

            birds.Clear();
            birds.AddRange(elements);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Entities
{
    public class MapEntity
    {
        public PointF position;
        public Size size;
        public int i;
        public int j;
        public int type;

        public MapEntity(PointF pos, Size size, int i, int j, int type)
        {
            this.position = pos;
            this.size = size;
            this.i = i;
            this.j = j;
            this.type = type;
        }
    }
}

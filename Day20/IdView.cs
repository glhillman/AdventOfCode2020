using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    public class IdView
    {
        public IdView(long id, int viewNum)
        {
            Id = id;
            ViewNum = viewNum;
        }    

        public long Id { get; set; }
        public int ViewNum { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, View: {1}", Id, ViewNum);
        }
    }

}

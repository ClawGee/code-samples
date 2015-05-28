using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class Projects
    {
        public Guid Uid { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public DateTime Launchdate { get; set; }

        public List<int> Technologies { get; set; }

        public int Id { get; set; }

        public void setTechnologiesString(string technologiesString)
        {
            if(technologiesString != null && technologiesString.Length > 0)
            {                
                List<string> techList = technologiesString.Split(',').ToList<string>();

                List<int> techIds = new List<int>();

                foreach(string tech in techList)
                {
                    if(tech.Length > 0)
                    {
                        techIds.Add(int.Parse(tech));
                    }
                }

                this.Technologies = techIds;
            }
        }
    }
}
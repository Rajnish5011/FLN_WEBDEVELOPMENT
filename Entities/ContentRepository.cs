using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreFLN_APIs.Entities
{
    public class ContentRepositoryCategory
    {
            public int Content_Category_Id { get; }
            public string Category_Name { get; }
            public string Category_Description { get; set; }
        }
    public class AppContentRepository
    {
        public int Content_Category_Id { get; }
        public int Content_Repository_Id { get; }
        public string Content_Title { get; set; }
        public string Content_Description { get; set; }
        public string Category_Name { get; }
        public string Category_Description { get; set; }
        public string Content_Url { get; set; }
        public int TotalRecords { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWMS.Routes
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Posts
        {
            public const string Object = "/posts/";

            public const string GetAll = "GETALLPOST";

            public const string GetPost = Base + "/posts/{postId}";

            public const string AddPost = Base + "/posts";

            public const string RemovePost = Base + "/posts/{postId}";

            public const string EditPost = Base + "/posts/{postId}";

            public const string ExportExcel = Base + "/posts/excel";

        }
    }
}

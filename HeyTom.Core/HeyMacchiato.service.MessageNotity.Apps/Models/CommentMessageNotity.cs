using HeyMachiato.Infra.RabbitMq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeyMacchiato.service.MessageNotity.Apps.Models
{
    [RabbitMq("MessageNotity","CommentMessage","direct","BlogComment")]
    public class CommentMessageNotity
    {
        public int blogId { get; set; }
        /// <summary>
        /// 文章标题
        /// </summary>
        public string blogName { get; set; }
        /// <summary>
        /// 评论Id
        /// </summary>
        public int commentId { get; set; }
        /// <summary>
        /// 评论人
        /// </summary>
        public int userId { get; set; }

        public string userName { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string message { get; set; }
    }
}

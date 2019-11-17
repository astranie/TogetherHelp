using BLL;
using SRC;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBFactory
{
    class SuggestFactory
    {
       
        private SuggestService suggestService;
        public SuggestFactory()
        {
            suggestService = new SuggestService();
        }
        public void Create()
        {
            //suggestService.Publish("小建议", "早点完工",RegisterFactory.Zhangsan.Id.ToString());
            //suggestService.Publish("第二个建议", "抓紧工作", RegisterFactory.Zhangsan.Id.ToString());
            //suggestService.Publish("第三个比较紧要的建议", "认真", RegisterFactory.Wangwu.Id.ToString());
        }
    }
}

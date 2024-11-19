using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class DashboardModel
    {
        public int NumDraft { get; set; }
        public int NumNew { get; set; }
        public int NumApproved { get; set; }
        public int NumRevise { get; set; }
        public int NumActive { get; set; }
		public int NumInActive { get; set; }
		public int NumCancel { get; set; }
        public int NumRecipientCancel { get; set; }
        public int NumWebAvail { get; set; }
        public int NumWebAdopt { get; set; }
        public int NumIn { get; set; }
        public int NumInBike { get; set; }
        public int NumInGifCard { get; set; }
        public int NumOut { get; set; }
        public int NumOutBike { get; set; }
        public int NumOutGfCard { get; set; }
        public int NumOther { get; set; }
        public int NumRecipientOther { get; set; }
        public int NumPending { get; set; }
    }
}
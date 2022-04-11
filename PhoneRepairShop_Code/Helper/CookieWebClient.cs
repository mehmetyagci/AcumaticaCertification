﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace PhoneRepairShop {
    public class CookieWebClient : WebClient {
        public CookieContainer CookieContainer { get; private set; }

        public CookieWebClient() {
            this.CookieContainer = new CookieContainer();
        }

        public CookieWebClient(CookieContainer cookieContainer) {
            this.CookieContainer = cookieContainer;
        }

        protected override WebRequest GetWebRequest(Uri address) {
            var request = base.GetWebRequest(address) as HttpWebRequest;
            if (request == null) return base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }
    }
}

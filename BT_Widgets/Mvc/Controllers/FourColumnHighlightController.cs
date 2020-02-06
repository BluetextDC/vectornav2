﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Mvc;
using SfImage = Telerik.Sitefinity.Libraries.Model.Image;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using BT_Widgets.Mvc.Models.FourColumnHighlight;

namespace BT_Widgets.Mvc.Controllers
{
    //[EnhanceViewEngines]
    [ControllerToolboxItem(Name = "FourColumnHighlight", Title = "4-Column-Highlight", SectionName = "Blue Text")]
    public class FourColumnHighlightController : Controller
    {
        #region Public properties

        /// <summary>
        /// Gets or sets the list title.
        /// </summary>
        /// <value>
        /// The list title.
        /// </value>
        public string ListTitle
        {
            get
            {
                return this.listTitle;
            }

            set
            {
                this.listTitle = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the html elements used for the list.
        /// </summary>
        /// <value>
        /// The type of the list.
        /// </value>
        public ListMode ListType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the list items.
        /// </summary>
        /// <value>
        /// The list items.
        /// </value>
        public string ListItems
        {
            get
            {
                return this.listItems;
            }

            set
            {
                this.listItems = value;
            }
        }
        public string ImageId
        {
            get
            {
                return this.imageId;
            }

            set
            {
                this.imageId = value;
            }
        }
        public string Link_Single { get; set; }
        public string Link { get; set; }
        public Guid? ImageId_Single
        {
            get;
            set;
        }
        public string Description { get => description; set => description = value; }
        #endregion

        #region Actions

        /// <summary>
        /// The default action
        /// </summary>
        /// <returns>The default widget view</returns>
        public ActionResult Index()
        {
            int cnt = 0;
            List<FourColumnHighlight> m_ListObject = new List<FourColumnHighlight>();
            this.ViewBag.ListTitle = this.ListTitle;
            this.ViewBag.ListType = this.ListType;

            foreach (var strval in this.DeserializeItems())
            {
                FourColumnHighlight m_object = new FourColumnHighlight();
                m_object.title = strval;

                m_object.GetImageSelected();
                m_ListObject.Add(m_object);
            }
            cnt = 0;
            foreach (var strval in this.DeserializeDescription())
            {
                try
                {
                    m_ListObject[cnt].description = strval;
                }
                catch
                {
                    //
                }
                cnt++;
            }
            cnt = 0;
            foreach (var strval in this.DeserializeImageId())
            {
                if (strval == null || strval.Trim().Length == 0)
                {
                    cnt++;
                    continue;
                }
                try
                {
                    m_ListObject[cnt].ImageId = new Guid(strval);
                    m_ListObject[cnt].GetImageSelected();
                }
                catch
                {
                    //
                }
                cnt++;
            }
            cnt = 0;
            foreach (var strval in this.DeserializeLinks())
            {
                if (strval == null || strval.Trim().Length == 0)
                {
                    cnt++;
                    continue;
                }
                try
                {
                    m_ListObject[cnt].CTALink = strval;
                }
                catch
                {
                    //
                }
                cnt++;
            }
            this.ViewBag.ListItems_Obj = m_ListObject;

            return this.View();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Deserialize the items.
        /// </summary>
        /// <returns>The list of items</returns>
        private IList<string> DeserializeItems()
        {
            var serializer = new JavaScriptSerializer();
            IList<string> items = new List<string>();

            if (!string.IsNullOrEmpty(this.ListItems))
                items = serializer.Deserialize<IList<string>>(this.ListItems);

            return items;
        }
        private IList<string> DeserializeDescription()
        {
            var serializer = new JavaScriptSerializer();
            IList<string> items = new List<string>();

            if (!string.IsNullOrEmpty(this.Description))
                items = serializer.Deserialize<IList<string>>(this.Description);

            return items;
        }
        private IList<string> DeserializeImageId()
        {
            var serializer = new JavaScriptSerializer();
            IList<string> items = new List<string>();

            if (!string.IsNullOrEmpty(this.ImageId))
                items = serializer.Deserialize<IList<string>>(this.ImageId);

            return items;
        }

        private IList<string> DeserializeLinks()
        {
            var serializer = new JavaScriptSerializer();
            IList<string> items = new List<string>();

            if (!string.IsNullOrEmpty(this.Link))
                items = serializer.Deserialize<IList<string>>(this.Link);

            return items;
        }
        #endregion

        #region Private fields and constants

        /// <summary>
        /// The list title
        /// </summary>
        private string listTitle = "My list title";

        /// <summary>
        /// The list items
        /// </summary>
        private string listItems = string.Empty;// "[\"First Item\", \"Second Item\", \"Third Item\"]";
        private string description = string.Empty;// "[\"desc 1\", \"desc2\", \"desc 3\"]";
        private string imageId = string.Empty;//"[\"37fca50d-d127-4b4e-9011-4964aaa94482\", \"37fca50d-d127-4b4e-9011-4964aaa94482\", \"37fca50d-d127-4b4e-9011-4964aaa94482\"]";
        #endregion
    }
}

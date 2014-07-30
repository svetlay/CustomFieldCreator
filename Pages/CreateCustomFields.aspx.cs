using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Catalog.Model;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Forums.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Modules.Newsletters;

namespace SitefinityWebApp.Pages
{
    public partial class MakeMeMeta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TypesComboBox.DataSource = commonSitefinityTypes;
                TypesComboBox.DataTextField = "Name";
                TypesComboBox.DataValueField = "type";
                
                ClrTypesBox.DataSource = commonCLRTypes;
                ClrTypesBox.DataTextField = "Name";
                ClrTypesBox.DataValueField = "type";

                SubscriberList.DataSource = GetSubscribers();
                SubscriberList.DataBind();

                TypesComboBox.DataBind();
                ClrTypesBox.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var metaManager = Telerik.Sitefinity.Data.Metadata.MetadataManager.GetManager();
            var type = this.typeToMetabolize;
            if (metaManager.GetMetaType(type) == null)
            {
                metaManager.CreateMetaType(type);
                metaManager.SaveChanges();

                MetaTypeResult.Text = String.Format("Type {0}'s Metabolism Successfully Improved", type.Name);
            }
            else
            {
                MetaTypeResult.Text = String.Format("Type {0} Was pretty Meta to begin with", type.Name);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var type = this.typeToMetabolize;
            var clrType = Type.GetType(ClrTypesBox.SelectedValue);

            var dynamicFields = App.WorkWith()
                                   .DynamicData()
                                   .Type(type);

            var fieldExists = dynamicFields.Fields().Get().Where(f => f.FieldName == TextBox1.Text).FirstOrDefault();

            if (fieldExists == null)
            {
                dynamicFields
                             .Field()
                             .TryCreateNew(TextBox1.Text, clrType)
                             .SaveChanges(true);

                MetaFieldResult.Text = String.Format("Field {0} was succesfully created", TextBox1.Text);
            }
            else
            {
                fieldExists.ClrType = ClrTypesBox.SelectedValue;
                dynamicFields.SaveChanges();

                MetaFieldResult.Text = String.Format("Field {0} was succesfully modified", TextBox1.Text);
            }

            if (clrType == typeof(Subscriber))
            {
                SubscriberTest.Visible = true;
            }
        }

        private List<Subscriber> GetSubscribers()
        {
            NewslettersManager manager = NewslettersManager.GetManager();
            var subscribers = manager.GetSubscribers().ToList();
            return subscribers;
        }

        Dictionary<string, Type> commonTypes
        {
            get
            {
                Dictionary<string, Type> dict = new Dictionary<string, Type>();
                dict.Add("int", typeof(int));
                dict.Add("string", typeof(string));
                dict.Add("Lstring", typeof(Lstring));
                dict.Add("bool", typeof(bool));

                return dict;
            }
        }

        public class MyMetaType
        {
            public string Name { get; set; }

            public Type type { get; set; }

            public MyMetaType(Type type)
            {
                this.type = type;
                this.Name = type.Name;
            }
        };

        #region Type Helpers

        public List<MyMetaType> commonCLRTypes
        {
            get
            {
                List<MyMetaType> metatypes = new List<MyMetaType>();
                metatypes.Add(new MyMetaType(typeof(int)));
                metatypes.Add(new MyMetaType(typeof(string)));
                metatypes.Add(new MyMetaType(typeof(bool)));
                metatypes.Add(new MyMetaType(typeof(Guid)));
                metatypes.Add(new MyMetaType(typeof(DateTime)));

                return metatypes;
            }
        }

        public List<MyMetaType> commonSitefinityTypes
        {
            get
            {
                List<MyMetaType> metatypes = new List<MyMetaType>();
                metatypes.Add(new MyMetaType(typeof(ProductVariation)));
                metatypes.Add(new MyMetaType(typeof(ProductAttribute)));
                metatypes.Add(new MyMetaType(typeof(Order)));
                metatypes.Add(new MyMetaType(typeof(Forum)));
                metatypes.Add(new MyMetaType(typeof(PageNode)));
                metatypes.Add(new MyMetaType(typeof(ForumThread)));
                metatypes.Add(new MyMetaType(typeof(ForumPost)));
                metatypes.Add(new MyMetaType(typeof(Subscriber)));
                metatypes.Add(new MyMetaType(typeof(Campaign)));

                return metatypes;
            }
        }

        private Type typeToMetabolize
        {
            get
            {
                return commonSitefinityTypes.Where(t => t.type.FullName == TypesComboBox.SelectedValue).FirstOrDefault().type;
            }
        }

        #endregion

        protected void ChangeCustomField_Click(object sender, EventArgs e)
        {
            NewslettersManager manager = NewslettersManager.GetManager();
            Subscriber subscriber = manager.GetSubscriber(new Guid(SubscriberList.SelectedValue));
            DataExtensions.SetValue((IDynamicFieldsContainer)subscriber, "Company", ComboBoxValueField.Text);

            SubscriberField.Text = String.Format("User {0} now works for Company {1}", SubscriberList.SelectedItem.Text, DataExtensions.GetValue<String>(subscriber, "Company"));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    /// <summary>
    /// BingSearchAPIのResponseModel
    /// </summary>
    public class BingSearchAPIResponseModel
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
        public partial class feed
        {

            private feedTitle titleField;

            private feedSubtitle subtitleField;

            private string idField;

            private feedRights rightsField;

            private System.DateTime updatedField;

            private feedLink linkField;

            private feedEntry[] entryField;

            /// <remarks/>
            public feedTitle title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public feedSubtitle subtitle
            {
                get
                {
                    return this.subtitleField;
                }
                set
                {
                    this.subtitleField = value;
                }
            }

            /// <remarks/>
            public string id
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            public feedRights rights
            {
                get
                {
                    return this.rightsField;
                }
                set
                {
                    this.rightsField = value;
                }
            }

            /// <remarks/>
            public System.DateTime updated
            {
                get
                {
                    return this.updatedField;
                }
                set
                {
                    this.updatedField = value;
                }
            }

            /// <remarks/>
            public feedLink link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("entry")]
            public feedEntry[] entry
            {
                get
                {
                    return this.entryField;
                }
                set
                {
                    this.entryField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedTitle
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedSubtitle
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedRights
        {

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedLink
        {

            private string relField;

            private string hrefField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rel
            {
                get
                {
                    return this.relField;
                }
                set
                {
                    this.relField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string href
            {
                get
                {
                    return this.hrefField;
                }
                set
                {
                    this.hrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedEntry
        {

            private object[] itemsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("content", typeof(feedEntryContent))]
            [System.Xml.Serialization.XmlElementAttribute("id", typeof(string))]
            [System.Xml.Serialization.XmlElementAttribute("link", typeof(feedEntryLink))]
            [System.Xml.Serialization.XmlElementAttribute("title", typeof(feedEntryTitle))]
            [System.Xml.Serialization.XmlElementAttribute("updated", typeof(System.DateTime))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedEntryContent
        {

            private properties propertiesField;

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public properties properties
            {
                get
                {
                    return this.propertiesField;
                }
                set
                {
                    this.propertiesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", IsNullable = false)]
        public partial class properties
        {

            private ID idField;

            private WebTotal webTotalField;

            private WebOffset webOffsetField;

            private ImageTotal imageTotalField;

            private ImageOffset imageOffsetField;

            private VideoTotal videoTotalField;

            private VideoOffset videoOffsetField;

            private NewsTotal newsTotalField;

            private NewsOffset newsOffsetField;

            private SpellingSuggestionsTotal spellingSuggestionsTotalField;

            private AlteredQuery alteredQueryField;

            private AlterationOverrideQuery alterationOverrideQueryField;

            private Title titleField;

            private MediaUrl mediaUrlField;

            private SourceUrl sourceUrlField;

            private Description descriptionField;

            private DisplayUrl displayUrlField;

            private RunTime runTimeField;

            private Width widthField;

            private Height heightField;

            private FileSize fileSizeField;

            private ContentType contentTypeField;

            private Thumbnail thumbnailField;

            private Url urlField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public ID ID
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public WebTotal WebTotal
            {
                get
                {
                    return this.webTotalField;
                }
                set
                {
                    this.webTotalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public WebOffset WebOffset
            {
                get
                {
                    return this.webOffsetField;
                }
                set
                {
                    this.webOffsetField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public ImageTotal ImageTotal
            {
                get
                {
                    return this.imageTotalField;
                }
                set
                {
                    this.imageTotalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public ImageOffset ImageOffset
            {
                get
                {
                    return this.imageOffsetField;
                }
                set
                {
                    this.imageOffsetField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public VideoTotal VideoTotal
            {
                get
                {
                    return this.videoTotalField;
                }
                set
                {
                    this.videoTotalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public VideoOffset VideoOffset
            {
                get
                {
                    return this.videoOffsetField;
                }
                set
                {
                    this.videoOffsetField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public NewsTotal NewsTotal
            {
                get
                {
                    return this.newsTotalField;
                }
                set
                {
                    this.newsTotalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public NewsOffset NewsOffset
            {
                get
                {
                    return this.newsOffsetField;
                }
                set
                {
                    this.newsOffsetField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public SpellingSuggestionsTotal SpellingSuggestionsTotal
            {
                get
                {
                    return this.spellingSuggestionsTotalField;
                }
                set
                {
                    this.spellingSuggestionsTotalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public AlteredQuery AlteredQuery
            {
                get
                {
                    return this.alteredQueryField;
                }
                set
                {
                    this.alteredQueryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public AlterationOverrideQuery AlterationOverrideQuery
            {
                get
                {
                    return this.alterationOverrideQueryField;
                }
                set
                {
                    this.alterationOverrideQueryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public Title Title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public MediaUrl MediaUrl
            {
                get
                {
                    return this.mediaUrlField;
                }
                set
                {
                    this.mediaUrlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public SourceUrl SourceUrl
            {
                get
                {
                    return this.sourceUrlField;
                }
                set
                {
                    this.sourceUrlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public Description Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public DisplayUrl DisplayUrl
            {
                get
                {
                    return this.displayUrlField;
                }
                set
                {
                    this.displayUrlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public RunTime RunTime
            {
                get
                {
                    return this.runTimeField;
                }
                set
                {
                    this.runTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public Width Width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public Height Height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public FileSize FileSize
            {
                get
                {
                    return this.fileSizeField;
                }
                set
                {
                    this.fileSizeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public ContentType ContentType
            {
                get
                {
                    return this.contentTypeField;
                }
                set
                {
                    this.contentTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public Thumbnail Thumbnail
            {
                get
                {
                    return this.thumbnailField;
                }
                set
                {
                    this.thumbnailField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
            public Url Url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class ID
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class WebTotal
        {

            private string typeField;

            private uint valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public uint Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class WebOffset
        {

            private string typeField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class ImageTotal
        {

            private string typeField;

            private ushort valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public ushort Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class ImageOffset
        {

            private string typeField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class VideoTotal
        {

            private string typeField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class VideoOffset
        {

            private string typeField;

            private byte valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public byte Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class NewsTotal
        {

            private string typeField;

            private bool nullField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public bool @null
            {
                get
                {
                    return this.nullField;
                }
                set
                {
                    this.nullField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class NewsOffset
        {

            private string typeField;

            private bool nullField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public bool @null
            {
                get
                {
                    return this.nullField;
                }
                set
                {
                    this.nullField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class SpellingSuggestionsTotal
        {

            private string typeField;

            private bool nullField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public bool @null
            {
                get
                {
                    return this.nullField;
                }
                set
                {
                    this.nullField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class AlteredQuery
        {

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class AlterationOverrideQuery
        {

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class Title
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class MediaUrl
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class SourceUrl
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class Description
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class DisplayUrl
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class RunTime
        {

            private string typeField;

            private uint valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public uint Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class Width
        {

            private string typeField;

            private ushort valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public ushort Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class Height
        {

            private string typeField;

            private ushort valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public ushort Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class FileSize
        {

            private string typeField;

            private uint valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public uint Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class ContentType
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class Thumbnail
        {

            private ThumbnailMediaUrl mediaUrlField;

            private ThumbnailContentType contentTypeField;

            private ThumbnailWidth widthField;

            private ThumbnailHeight heightField;

            private ThumbnailFileSize fileSizeField;

            private string typeField;

            /// <remarks/>
            public ThumbnailMediaUrl MediaUrl
            {
                get
                {
                    return this.mediaUrlField;
                }
                set
                {
                    this.mediaUrlField = value;
                }
            }

            /// <remarks/>
            public ThumbnailContentType ContentType
            {
                get
                {
                    return this.contentTypeField;
                }
                set
                {
                    this.contentTypeField = value;
                }
            }

            /// <remarks/>
            public ThumbnailWidth Width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            public ThumbnailHeight Height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }

            /// <remarks/>
            public ThumbnailFileSize FileSize
            {
                get
                {
                    return this.fileSizeField;
                }
                set
                {
                    this.fileSizeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public partial class ThumbnailMediaUrl
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public partial class ThumbnailContentType
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public partial class ThumbnailWidth
        {

            private string typeField;

            private ushort valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public ushort Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public partial class ThumbnailHeight
        {

            private string typeField;

            private ushort valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public ushort Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        public partial class ThumbnailFileSize
        {

            private string typeField;

            private ushort valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public ushort Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices", IsNullable = false)]
        public partial class Url
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedEntryLink
        {

            private inline inlineField;

            private string relField;

            private string typeField;

            private string titleField;

            private string hrefField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
            public inline inline
            {
                get
                {
                    return this.inlineField;
                }
                set
                {
                    this.inlineField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string rel
            {
                get
                {
                    return this.relField;
                }
                set
                {
                    this.relField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string href
            {
                get
                {
                    return this.hrefField;
                }
                set
                {
                    this.hrefField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata", IsNullable = false)]
        public partial class inline
        {

            private feed feedField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/Atom")]
            public feed feed
            {
                get
                {
                    return this.feedField;
                }
                set
                {
                    this.feedField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
        public partial class feedEntryTitle
        {

            private string typeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }
    }
}
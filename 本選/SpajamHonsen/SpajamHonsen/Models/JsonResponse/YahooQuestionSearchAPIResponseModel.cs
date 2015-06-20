using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpajamHonsen.Models.JsonResponse
{
    /// <summary>
    /// YahooQuestionSearchAPIのXMLResponseModel
    /// </summary>
    public class YahooQuestionSearchAPIResponseModel
    {
        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:yahoo:jp:chiebukuro")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "urn:yahoo:jp:chiebukuro", IsNullable = false)]
        public partial class ResultSet
        {

            private ResultSetStatus statusField;

            private ResultSetQuestion[] resultField;

            private byte totalResultsReturnedField;

            private ushort totalResultsAvailableField;

            private byte firstResultPositionField;

            /// <remarks/>
            public ResultSetStatus Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Question", IsNullable = false)]
            public ResultSetQuestion[] Result
            {
                get
                {
                    return this.resultField;
                }
                set
                {
                    this.resultField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte totalResultsReturned
            {
                get
                {
                    return this.totalResultsReturnedField;
                }
                set
                {
                    this.totalResultsReturnedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort totalResultsAvailable
            {
                get
                {
                    return this.totalResultsAvailableField;
                }
                set
                {
                    this.totalResultsAvailableField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte firstResultPosition
            {
                get
                {
                    return this.firstResultPositionField;
                }
                set
                {
                    this.firstResultPositionField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:yahoo:jp:chiebukuro")]
        public partial class ResultSetStatus
        {

            private byte statusCodeField;

            private object errorField;

            /// <remarks/>
            public byte StatusCode
            {
                get
                {
                    return this.statusCodeField;
                }
                set
                {
                    this.statusCodeField = value;
                }
            }

            /// <remarks/>
            public object Error
            {
                get
                {
                    return this.errorField;
                }
                set
                {
                    this.errorField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:yahoo:jp:chiebukuro")]
        public partial class ResultSetQuestion
        {

            private ulong idField;

            private string contentField;

            private string bestAnswerField;

            private string urlField;

            private string conditionField;

            private string categoryPathField;

            private string categoryIdPathField;

            private byte ansCountField;

            private System.DateTime postedDateField;

            private string solvedDateField;

            private string postedDeviceField;

            /// <remarks/>
            public ulong Id
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
            public string Content
            {
                get
                {
                    return this.contentField;
                }
                set
                {
                    this.contentField = value;
                }
            }

            /// <remarks/>
            public string BestAnswer
            {
                get
                {
                    return this.bestAnswerField;
                }
                set
                {
                    this.bestAnswerField = value;
                }
            }

            /// <remarks/>
            public string Url
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

            /// <remarks/>
            public string Condition
            {
                get
                {
                    return this.conditionField;
                }
                set
                {
                    this.conditionField = value;
                }
            }

            /// <remarks/>
            public string CategoryPath
            {
                get
                {
                    return this.categoryPathField;
                }
                set
                {
                    this.categoryPathField = value;
                }
            }

            /// <remarks/>
            public string CategoryIdPath
            {
                get
                {
                    return this.categoryIdPathField;
                }
                set
                {
                    this.categoryIdPathField = value;
                }
            }

            /// <remarks/>
            public byte AnsCount
            {
                get
                {
                    return this.ansCountField;
                }
                set
                {
                    this.ansCountField = value;
                }
            }

            /// <remarks/>
            public System.DateTime PostedDate
            {
                get
                {
                    return this.postedDateField;
                }
                set
                {
                    this.postedDateField = value;
                }
            }

            /// <remarks/>
            public string SolvedDate
            {
                get
                {
                    return this.solvedDateField;
                }
                set
                {
                    this.solvedDateField = value;
                }
            }

            /// <remarks/>
            public string PostedDevice
            {
                get
                {
                    return this.postedDeviceField;
                }
                set
                {
                    this.postedDeviceField = value;
                }
            }
        }
    }
}
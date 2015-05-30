using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;

namespace SampleBase.Base
{
	/// <summary>
	/// XMLを操作するユーティリティクラスです。
	/// </summary>
	public static class XmlUtilities
	{
		/// <summary>
		/// 指定されたXML文字列をXmlElementに変換します。
		/// </summary>
		/// <param name="xmlString">XML文字列</param>
		/// <returns>XmlElement</returns>
		/// <remarks>XML文字列がnullの場合はnullが返されます。</remarks>
		public static XmlElement ToXmlElement(this string xmlString)
		{
			if (xmlString == null)
			{
				return null;
			}

			var document = new XmlDocument();
			document.LoadXml(xmlString);

			return document.DocumentElement;
		}

		/// <summary>
		/// 指定されたXML文字列をXmlDocumentに変換します。
		/// </summary>
		/// <param name="xmlString">XML文字列</param>
		/// <returns>XmlDocument</returns>
		/// <remarks>XML文字列がnullの場合はnullが返されます。</remarks>
		public static XmlDocument ToXmlDocument(this string xmlString)
		{
			if (xmlString == null)
			{
				return null;
			}

			var document = new XmlDocument();
			document.LoadXml(xmlString);

			return document;
		}

		/// <summary>
		/// 指定されたXML文字列をXElementに変換します。
		/// </summary>
		/// <param name="xmlString">XML文字列</param>
		/// <returns>XElement</returns>
		/// <remarks>XML文字列がnullの場合はnullが返されます。</remarks>
		public static XElement ToXElement(this string xmlString)
		{
			if (xmlString == null)
			{
				return null;
			}

			return XElement.Parse(xmlString);
		}

		/// <summary>
		/// 指定されたXML文字列をXDocumentに変換します。
		/// </summary>
		/// <param name="xmlString">XML文字列</param>
		/// <returns>XDocument</returns>
		/// <remarks>XML文字列がnullの場合はnullが返されます。</remarks>
		public static XDocument ToXDocument(this string xmlString)
		{
			if (xmlString == null)
			{
				return null;
			}

			return XDocument.Parse(xmlString);
		}

		/// <summary>
		/// インスタンスをXamlに変換します。
		/// </summary>
		/// <param name="instance">インスタンス</param>
		/// <returns>Xamlを示すXElement</returns>
		public static XElement ToXamlElement(this object instance)
		{
			Debug.Assert(instance != null);

			using (var ms = new MemoryStream())
			{
				XamlWriter.Save(instance, ms);
				ms.Position = 0;
				return XElement.Load(ms);
			}
		}

		/// <summary>
		/// ストリームからXamlを読み取り、インスタンスを取得します。
		/// </summary>
		/// <param name="stream">ストリーム</param>
		/// <returns>インスタンス</returns>
		public static object ParseXamlElement(this Stream stream)
		{
			Debug.Assert(stream != null);

			XamlReader reader = new XamlReader();

			return reader.LoadAsync(stream);
		}
	}
}

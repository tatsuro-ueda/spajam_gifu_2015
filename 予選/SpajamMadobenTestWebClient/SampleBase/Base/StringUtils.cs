using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SampleBase.Base
{
	/// <summary>
	/// 文字列を操作するユーティリティクラスです。
	/// </summary>
	public static class StringUtilities
	{
		#region Fields
		private static readonly char[] vowels_ = new[] { 'a', 'i', 'u', 'e', 'o' };
		private static readonly Dictionary<string, Color> colors_ = new Dictionary<string, Color>(StringComparer.InvariantCultureIgnoreCase);
		#endregion

		#region Type initializer
		/// <summary>
		/// タイプイニシャライザーです。
		/// </summary>
		static StringUtilities()
		{
			foreach (var pi in typeof(Colors).
				GetProperties(BindingFlags.Public | BindingFlags.Static).
				Where(pi => (pi.CanRead == true) && (pi.PropertyType == typeof(Color)) && (pi.GetIndexParameters().Length == 0)))
			{
				var color = (Color)pi.GetValue(null);
				colors_.Add(pi.Name, color);
			}
		}
		#endregion

		#region PrepareParsing
		/// <summary>
		/// 文字列をパースします。
		/// </summary>
		/// <param name="stringValue">パースする文字列</param>
		/// <param name="spliceChar">区切り文字</param>
		/// <param name="action">文字列の切り出し位置が特定される度に呼び出されるデリゲート</param>
		/// <remarks>
		/// デリゲートは、文字列を切り出すべき位置と長さを通知します。また、デリゲートの戻り値がfalseの場合、そこで処理を中止します。
		/// </remarks>
		/// <example>
		/// <code>
		/// // 文字列をカンマで区切り、単語をコンソールに出力する
		/// var stringValue = "ABC,DEF,GHI,JKL";
		/// stringValue.PrepareParsing(',', (sv, start, count) =>
		///		{
		///			Console.WriteLine(sv.Substring(start, count));
		///			return true;
		///		});
		/// </code>
		/// </example>
		public static void PrepareParsing(
			this string stringValue,
			char spliceChar,
			Func<string, int, int, bool> action)
		{
			Debug.Assert(stringValue != null);
			Debug.Assert(action != null);

			if (stringValue.Length == 0)
			{
				return;
			}

			var startIndex = 0;
			while (true)
			{
				var index = stringValue.IndexOf(spliceChar, startIndex);
				if (index == -1)
				{
					action(stringValue, startIndex, stringValue.Length - startIndex);
					break;
				}
				else
				{
					if (action(stringValue, startIndex, index - startIndex) == false)
					{
						break;
					}
				}

				startIndex = index + 1;
			}
		}

		/// <summary>
		/// 文字列をパースします。
		/// </summary>
		/// <param name="stringValue">パースする文字列</param>
		/// <param name="spliceChar">区切り文字</param>
		/// <returns>パースに必要な文字列の切り出し位置を返す列挙子</returns>
		/// <example>
		/// <code>
		/// // 文字列をカンマで区切り、単語をコンソールに出力する
		/// var stringValue = "ABC,DEF,GHI,JKL";
		/// foreach (var entry in stringValue.PrepareParsing(','))
		/// {
		///		Console.WriteLine(entry.Item1.Substring(entry.Item2, entry.Item3));
		/// }
		/// </code>
		/// </example>
		public static IEnumerable<Tuple<string, int, int>> PrepareParsing(
			this string stringValue,
			char spliceChar)
		{
			Debug.Assert(stringValue != null);

			if (stringValue.Length == 0)
			{
				yield break;
			}

			var startIndex = 0;
			while (true)
			{
				var index = stringValue.IndexOf(spliceChar, startIndex);
				if (index == -1)
				{
					var count = stringValue.Length - startIndex;
					yield return Tuple.Create(stringValue, startIndex, count);

					break;
				}
				else
				{
					var count = index - startIndex;
					yield return Tuple.Create(stringValue, startIndex, count);
				}

				startIndex = index + 1;
			}
		}
		#endregion

		#region PrepareParsingMultiple
		/// <summary>
		/// 文字列をパースします。
		/// </summary>
		/// <param name="stringValue">パースする文字列</param>
		/// <param name="spliceChar">区切り文字</param>
		/// <param name="actions">文字列の切り出し位置が特定される度に呼び出されるデリゲート群</param>
		/// <remarks>
		/// デリゲートは、文字列を切り出すべき位置と長さを通知します。また、デリゲートの戻り値がfalseの場合、そこで処理を中止します。
		/// 処理の中断が必要ない場合は、別のオーバーロードを使用すると、簡潔に記述する事が出来ます。
		/// </remarks>
		/// <example>
		/// <code>
		/// // 文字列をカンマで区切り、単語を3個までコンソールに出力する
		/// var stringValue = "ABC,DEF,GHI,JKL";
		/// stringValue.PrepareParsingMultiple(',',
		///		(sv, start, count) =>
		///		{
		///			Console.WriteLine("First: ", sv.Substring(start, count));
		///			return true;
		///		},
		///		(sv, start, count) =>
		///		{
		///			Console.WriteLine("Second: ", sv.Substring(start, count));
		///			return true;
		///		},
		///		(sv, start, count) =>
		///		{
		///			Console.WriteLine("Third: ", sv.Substring(start, count));
		///			return false;
		///		});
		/// </code>
		/// </example>
		public static void PrepareParsingMultiple(
			this string stringValue,
			char spliceChar,
			params Func<string, int, int, bool>[] actions)
		{
			Debug.Assert(stringValue != null);

			if (stringValue.Length == 0)
			{
				return;
			}

			var actionIndex = 0;
			var startIndex = 0;
			while (actionIndex < actions.Length)
			{
				var action = actions[actionIndex];
				Debug.Assert(action != null);

				var index = stringValue.IndexOf(spliceChar, startIndex);
				if (index == -1)
				{
					action(stringValue, startIndex, stringValue.Length - startIndex);
					actionIndex++;
					break;
				}
				else
				{
					if (action(stringValue, startIndex, index - startIndex) == false)
					{
						break;
					}
				}

				startIndex = index + 1;
				actionIndex++;
			}
		}

		/// <summary>
		/// 文字列をパースします。
		/// </summary>
		/// <param name="stringValue">パースする文字列</param>
		/// <param name="spliceChar">区切り文字</param>
		/// <param name="actions">文字列の切り出し位置が特定される度に呼び出されるデリゲート群</param>
		/// <returns>処理されたアクションデリゲート数</returns>
		/// <remarks>
		/// デリゲートは、文字列を切り出すべき位置と長さを通知します。
		/// 途中で解析を中断させる場合は、別のオーバーロードを使用して下さい。
		/// </remarks>
		/// <example>
		/// <code>
		/// // 文字列をカンマで区切り、単語を3個までコンソールに出力する
		/// var stringValue = "ABC,DEF,GHI,JKL";
		/// stringValue.PrepareParsingMultiple(',',
		///		(sv, start, count) => Console.WriteLine("First: ", sv.Substring(start, count)),
		///		(sv, start, count) => Console.WriteLine("Second: ", sv.Substring(start, count)),
		///		(sv, start, count) => Console.WriteLine("Third: ", sv.Substring(start, count)));
		/// </code>
		/// </example>
		public static int PrepareParsingMultiple(
			this string stringValue,
			char spliceChar,
			params Action<string, int, int>[] actions)
		{
			Debug.Assert(stringValue != null);

			if (stringValue.Length == 0)
			{
				return 0;
			}

			var actionIndex = 0;
			var startIndex = 0;
			while (actionIndex < actions.Length)
			{
				var action = actions[actionIndex];
				Debug.Assert(action != null);

				var index = stringValue.IndexOf(spliceChar, startIndex);
				if (index == -1)
				{
					action(stringValue, startIndex, stringValue.Length - startIndex);
					actionIndex++;
					break;
				}
				else
				{
					action(stringValue, startIndex, index - startIndex);
				}

				startIndex = index + 1;
				actionIndex++;
			}

			return actionIndex;
		}
		#endregion

		#region Parse
		/// <summary>
		/// 文字列をパースします。
		/// </summary>
		/// <param name="stringValue">パースする文字列</param>
		/// <param name="spliceChar">区切り文字</param>
		/// <param name="action">文字列が切り出される度に呼び出されるデリゲート</param>
		/// <param name="option">オプション</param>
		/// <remarks>
		/// デリゲートの戻り値がfalseの場合、そこで処理を中止します。
		/// </remarks>
		/// <example>
		/// <code>
		/// // 文字列をカンマで区切り、単語をコンソールに出力する
		/// var stringValue = "ABC,DEF,GHI,JKL";
		/// stringValue.Parse(',', value =>
		///		{
		///			Console.WriteLine(value);
		///			return true;
		///		});
		/// </code>
		/// </example>
		public static void Parse(
			this string stringValue,
			char spliceChar,
			Func<string, bool> action,
			StringSplitOptions option = StringSplitOptions.None)
		{
			Debug.Assert(stringValue != null);
			Debug.Assert(action != null);

			if (option == StringSplitOptions.RemoveEmptyEntries)
			{
				stringValue.PrepareParsing(
					spliceChar,
					(sv, start, count) =>
					{
						if (count == 0)
						{
							return true;
						}

						var value = sv.Substring(start, count);
						if (string.IsNullOrWhiteSpace(value) == true)
						{
							return true;
						}

						return action(value);
					});
			}
			else
			{
				stringValue.PrepareParsing(
					spliceChar,
					(sv, start, count) => action(sv.Substring(start, count)));
			}
		}

		/// <summary>
		/// 文字列をパースします。
		/// </summary>
		/// <param name="stringValue">パースする文字列</param>
		/// <param name="spliceChar">区切り文字</param>
		/// <param name="option">オプション</param>
		/// <returns>パースした結果を返す列挙子</returns>
		/// <example>
		/// <code>
		/// // 文字列をカンマで区切り、単語をコンソールに出力する
		/// var stringValue = "ABC,DEF,GHI,JKL";
		/// foreach (var value in stringValue.Parse(','))
		/// {
		///		Console.WriteLine(value);
		/// }
		/// </code>
		/// </example>
		public static IEnumerable<string> Parse(
			this string stringValue,
			char spliceChar,
			StringSplitOptions option = StringSplitOptions.None)
		{
			Debug.Assert(stringValue != null);

			if (option == StringSplitOptions.RemoveEmptyEntries)
			{
				return
					from entry in stringValue.PrepareParsing(spliceChar)
					where entry.Item3 >= 1
					let value = entry.Item1.Substring(entry.Item2, entry.Item3)
					where string.IsNullOrWhiteSpace(value) == false
					select value;
			}
			else
			{
				return
					from entry in stringValue.PrepareParsing(spliceChar)
					select entry.Item1.Substring(entry.Item2, entry.Item3);
			}
		}
		#endregion

		//#region ParseJsonString
		///// <summary>
		///// 指定されたJson文字列からインスタンスを復元します。
		///// </summary>
		///// <typeparam name="T">復元対象の型</typeparam>
		///// <param name="jsonString">Json文字列</param>
		///// <returns>インスタンス</returns>
		//public static T ParseJsonString<T>(this string jsonString)
		//{
		//	Debug.Assert(jsonString != null);

		//	var formatter = new JsonFormatter(typeof(T));
		//	return (T)formatter.Deserialize(jsonString);
		//}
		//#endregion

		//#region ToJsonString
		///// <summary>
		///// 指定されたインスタンスをJson文字列に変換します。
		///// </summary>
		///// <typeparam name="T">インスタンスの型</typeparam>
		///// <param name="instance">インスタンス</param>
		///// <returns>Json文字列</returns>
		//public static string ToJsonString<T>(this T instance)
		//{
		//	if (instance == null)
		//	{
		//		return "null";
		//	}

		//	var formatter = new JsonFormatter(instance.GetType());
		//	return formatter.Serialize(instance);
		//}
		//#endregion

		#region ToInt32
		/// <summary>
		/// 文字列をInt32に変換します。
		/// </summary>
		/// <param name="stringValue">文字列</param>
		/// <param name="defaultValue">変換できない場合の既定値</param>
		/// <returns>値</returns>
		public static int ToInt32(this string stringValue, int defaultValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue) == true)
			{
				return defaultValue;
			}

			int value;
			if (int.TryParse(stringValue, out value) == true)
			{
				return value;
			}

			return defaultValue;
		}
		#endregion

		#region ToInt64
		/// <summary>
		/// 文字列をInt64に変換します。
		/// </summary>
		/// <param name="stringValue">文字列</param>
		/// <param name="defaultValue">変換できない場合の既定値</param>
		/// <returns>値</returns>
		public static long ToInt64(this string stringValue, long defaultValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue) == true)
			{
				return defaultValue;
			}

			long value;
			if (long.TryParse(stringValue, out value) == true)
			{
				return value;
			}

			return defaultValue;
		}
		#endregion

		#region ToDouble
		/// <summary>
		/// 文字列をDoubleに変換します。
		/// </summary>
		/// <param name="stringValue">文字列</param>
		/// <param name="defaultValue">変換できない場合の既定値</param>
		/// <returns>値</returns>
		public static double ToDouble(this string stringValue, double defaultValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue) == true)
			{
				return defaultValue;
			}

			double value;
			if (double.TryParse(stringValue, out value) == true)
			{
				return value;
			}

			return defaultValue;
		}
		#endregion

		#region ToColor
		/// <summary>
		/// 色文字列をColor型に変換します。
		/// </summary>
		/// <param name="colorName">色文字列</param>
		/// <returns>Color型</returns>
		public static Color ToColor(this string colorName)
		{
			if (string.IsNullOrWhiteSpace(colorName) == true)
			{
				return Colors.White;
			}

			if (colorName.StartsWith("#") == true)
			{
				var systemColor = System.Drawing.ColorTranslator.FromHtml(colorName);
				return Color.FromArgb(systemColor.A, systemColor.R, systemColor.G, systemColor.B);
			}
			else
			{
				return colors_[colorName];
			}
		}
		#endregion

		#region ToEnum
		/// <summary>
		/// 指定された文字列を列挙型の値に変換します。
		/// </summary>
		/// <typeparam name="T">列挙型</typeparam>
		/// <param name="enumValue">列挙値を示す文字列</param>
		/// <returns>値</returns>
		public static T ToEnum<T>(this string enumValue)
			where T : struct
		{
			Debug.Assert(typeof(T).IsEnum == true);

			if (string.IsNullOrWhiteSpace(enumValue) == true)
			{
				return default(T);
			}

			T value;
			if (Enum.TryParse<T>(enumValue, out value) == true)
			{
				return value;
			}

			var underlyingType = Enum.GetUnderlyingType(typeof(T));
			try
			{
				var numericValue = Convert.ChangeType(enumValue, underlyingType, CultureInfo.InvariantCulture);
				return (T)Enum.ToObject(typeof(T), numericValue);
			}
			catch
			{
			}

			return default(T);
		}
		#endregion

		#region MakePluralForm
		/// <summary>
		/// 複数形を作ります。
		/// </summary>
		/// <param name="word">単語</param>
		/// <returns>単語</returns>
		public static string MakePluralForm(this string word)
		{
			Debug.Assert(string.IsNullOrWhiteSpace(word) == false);
			Debug.Assert(word.Contains(' ') == false);

			// （既に複数形：不完全である事に注意）
			if (word.EndsWith("es"))
			{
				return word;
			}

			if (word.EndsWith("y") == true)
			{
				if (word.Length >= 2)
				{
					var lasttwo = word[word.Length - 2];
					if (vowels_.Contains(lasttwo) == false)
					{
						return word.Substring(0, word.Length - 1) + "ies";
					}
				}
			}

			if (word.EndsWith("s") || word.EndsWith("sh") || word.EndsWith("ch") || word.EndsWith("o") || word.EndsWith("x"))
			{
				return word + "es";
			}

			return word + "s";
		}
		#endregion
	}
}

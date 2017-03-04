using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Ultralpha
{
    public class PathBinding : MonoBehaviour
    {
        [SerializeField] private Text _text;

#if !UNITY_5
    /// <summary>
    /// In unity 4.6 this <see cref="HelpHeaderAttribute"/> will be drawn multiple times(bug?)
    /// </summary>
#endif
        [HelpHeader("Binding path(use '.' for nested path. Max depth is 2)")] [RegexCheckField("^[A-Za-z@_][A-Za-z0-9_]*(.[A-Za-z@_][A-Za-z0-9_]*){0,1}$", warning = "Invalid path expression"
            )] [SerializeField] private string _path;

        private object _dataSource;

        /// <summary>
        /// Text control to display binded value
        /// </summary>
        public Text Text
        {
            get { return _text; }
            set
            {
                _text = value;
                UpdateView();
            }
        }

        /// <summary>
        /// Binding path(use dot"." for nested path. Max depth is 2)
        /// </summary>
        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                UpdateView();
            }
        }

        /// <summary>
        /// Data binding source
        /// </summary>
        public object DataSource
        {
            get { return _dataSource; }
            set
            {
                _dataSource = value;
                UpdateView();
            }
        }

        /// <summary>
        /// Update View
        /// </summary>
        protected virtual void UpdateView()
        {
            if (_text == null)
                return;
            if (_dataSource == null)
            {
                _text.text = "[NULL]";
                return;
            }
            if (string.IsNullOrEmpty(_path))
            {
                _text.text = _dataSource.ToString();
                return;
            }

            string[] paths = _path.Split(new[] {'.'}, StringSplitOptions.RemoveEmptyEntries);
            if (paths.Length == 1)
            {
                object value = GetMemberValue(_dataSource, paths[0]);
                _text.text = value == null ? "[NULL]" : value.ToString();
            }
            else if (paths.Length == 2)
            {
                object value = GetMemberValue(_dataSource, paths[0]);
                if (value != null)
                {
                    object subValue = GetMemberValue(value, paths[1]);
                    _text.text = subValue == null ? "[NULL]" : subValue.ToString();
                }
                else
                {
                    _text.text = "[NULL]";
                }
            }
            else
            {
                throw new ArgumentException("Invalid binding path");
            }
        }

        /// <summary>
        /// Get filed/property from target object
        /// </summary>
        /// <param name="data"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        protected object GetMemberValue(object data, string memberName)
        {
            if (data is IEnumerable || data.GetType().Namespace == typeof (object).Namespace)
                return data.ToString();
            MemberInfo[] members = data.GetType().GetMember(memberName, MemberTypes.Property | MemberTypes.Field,
                BindingFlags.Public | BindingFlags.Instance);
            if (members.Length != 1)
                throw new ArgumentException("Binding path not found or more than one are found");
            return members[0].MemberType == MemberTypes.Field
                ? ((FieldInfo) members[0]).GetValue(data)
                : ((PropertyInfo) members[0]).GetValue(data, null);
        }
    }
}
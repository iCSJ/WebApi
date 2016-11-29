using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Data;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Command;

namespace CyWpf.ViewModel
{
    public delegate object CallbackHandler(params object[] p);
    /// <summary>
    /// 带数据有效性验证的视图模型基类
    /// </summary>
    public abstract class ValidationViewModelBase : ViewModelBase, IDataErrorInfo
    {
        private Dictionary<string, string> errorDictionary;
        public Dictionary<string, string> ErrorDictionary
        {
            get
            {
                if (null == errorDictionary)
                {
                    errorDictionary = new Dictionary<string, string>();
                }
                return errorDictionary;
            }
        }
        /// <summary>
        /// 是否启用提前验证，启用后会在用户未输入数据前进行输入验证，否则在用户输入后才进行验证
        /// </summary>
        public bool PreValidation { get; set; }
        public string this[string columnName]
        {
            get
            {
                if (PreValidation)
                {
                    return this.ValidateProperty(columnName);
                }
                if (ErrorDictionary.ContainsKey(columnName))
                {
                    return ErrorDictionary[columnName];
                }
                return string.Empty;
            }

        }

        internal void AddError(string propertyName, string errorMessage)
        {
            ErrorDictionary[propertyName] = errorMessage;
            RaisePropertyChanged("Error");
        }

        internal void RemoveError(string propertyName)
        {
            ErrorDictionary.Remove(propertyName);
            RaisePropertyChanged("Error");
        }

        public string Error
        {
            get
            {
                if (ErrorDictionary.Count > 0)
                {
                    return string.Join(Environment.NewLine, ErrorDictionary.Values);
                }
                return string.Empty;
            }
        }

        public void NotityProperChanged(string propertyName)
        {
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// 释放资源,注销ViewModel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public virtual void OnUnregister<T>(object obj) where T : class
        {
            SimpleIoc.Default.Unregister<T>();
            SimpleIoc.Default.Register<T>();
        }
    }
    /// <summary>
    /// 验证功能的扩展类
    /// </summary>
    public static class ValidationExtension
    {
        /// <summary>
        /// 验证ViewModel的某一个属性的有效性
        /// </summary>
        /// <param name="dp"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string ValidateProperty(this ValidationViewModelBase dp, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return string.Empty;
            var targetType = dp.GetType();
            var propertyValue = targetType.GetProperty(propertyName).GetValue(dp, null);
            return dp.ValidateProperty(propertyValue, propertyName);
        }

        /// <summary>
        /// 验证ViewModel的某一个属性的有效性
        /// </summary>
        /// <typeparam name="MetadataType"></typeparam>
        /// <param name="dp"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string ValidateProperty<MetadataType>(this ValidationViewModelBase dp, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return string.Empty;

            var targetType = dp.GetType();
            if (targetType != typeof(MetadataType))
            {
                TypeDescriptor.AddProviderTransparent(
                       new AssociatedMetadataTypeTypeDescriptionProvider(targetType, typeof(MetadataType)), targetType);
            }

            var propertyValue = targetType.GetProperty(propertyName).GetValue(dp, null);
            return dp.ValidateProperty(propertyValue, propertyName);
        }
        /// <summary>
        /// 验证ViewModel的某一个属性的有效性
        /// </summary>
        /// <param name="dp">ViewModel</param>
        /// <param name="value">值</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns></returns>
        public static string ValidateProperty(this ValidationViewModelBase dp, object value, string propertyName)
        {
            ValidationContext vc = new ValidationContext(dp, null, null) { MemberName = propertyName };
            List<System.ComponentModel.DataAnnotations.ValidationResult> vr = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            if (Validator.TryValidateProperty(value, vc, vr))
            {
                dp.RemoveError(propertyName);
                return string.Empty;
            }
            else
            {
                dp.AddError(propertyName, vr.FirstOrDefault().ErrorMessage);
                return vr.FirstOrDefault().ErrorMessage;
            }
        }
        /// <summary>
        /// 验证整个ViewModel的有性性
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static bool ValidateViewModel(this ValidationViewModelBase dp)
        {
            string strValid = string.Empty;
            var props = dp.GetType()
                .GetProperties()
                .Where(p => ((ValidationAttribute[])p.GetCustomAttributes(typeof(ValidationAttribute), true)).Length != 0);
            foreach (PropertyInfo item in props)
            {
                var value = item.GetValue(dp, null);
                strValid = dp.ValidateProperty(value, item.Name);
                dp.NotityProperChanged(item.Name);
                if (strValid != string.Empty)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 验证控件是否出现错误信息
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool HasError(this DependencyObject node)
        {
            if (node != null)
            {
                FrameworkElement fe = node as TextBox;
                //if (fe != null)
                //{
                //    fe.ValidBindingExpression("Text");
                //}
                bool hasErr = System.Windows.Controls.Validation.GetHasError(node);
                if (hasErr)
                {
                    if (node is IInputElement) Keyboard.Focus((IInputElement)node);
                    return true;
                }
            }

            foreach (object subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (subnode is DependencyObject)
                {
                    if (HasError((DependencyObject)subnode) == true) return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 通过属性名称获取依懒属性并进行验证
        /// </summary>
        /// <param name="fe"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool ValidBindingExpression(this FrameworkElement fe, string propertyName)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(fe);
            PropertyDescriptor myProperty = properties[propertyName];
            if (myProperty == null) return true;
            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(myProperty);
            if (dpd == null) return true;
            DependencyProperty dp = dpd.DependencyProperty;
            BindingExpression be = fe.GetBindingExpression(dp);
            //be.UpdateSource();
            if (System.Windows.Controls.Validation.GetHasError(fe))
                MessageBox.Show((System.Windows.Controls.Validation.GetErrors(fe))[0].ErrorContent.ToString());
            return true;
        }
    }
}

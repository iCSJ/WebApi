//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;

//namespace CyWpf.ViewModel
//{
//    public interface IWindowFactory
//    {
//        void CreateWindow();
//        bool? CreateDialogWindow( OkCancelHandler ok = null, OkCancelHandler cancel = null);
//    }
//    public abstract class BaseWindowFactory : IWindowFactory
//    {
//        public void CreateWindow()
//        {
//            BaseWindow w = GetWindow();
//            w.Show();
//        }

//        public bool? CreateDialogWindow(OkCancelHandler ok = null, OkCancelHandler cancel = null)
//        {
//            BaseWindow w = GetWindow();
//            w.OKCallback += ok;
//            w.CancelCallback += cancel;
//            return w.ShowDialog();
//        }

//        protected virtual BaseWindow GetWindow()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicEditor.Helpers
{
    class MessageHelper
    {
        public static void ErrorMessage(string msg) {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static Boolean QuestionMessage (string msg){
            DialogResult result = MessageBox.Show(msg, "Pregunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return (result is DialogResult.Yes) ? true : false;    
        }

        public static void InfoMessage(string msg) {
            MessageBox.Show(msg);
        }

        public static Boolean StandardMessage(string msg, string title,MessageBoxIcon icon)
        {
            DialogResult result = MessageBox.Show(msg, title, MessageBoxButtons.YesNo, icon);
            return (result is DialogResult.Yes) ? true : false;
        }
    }
}

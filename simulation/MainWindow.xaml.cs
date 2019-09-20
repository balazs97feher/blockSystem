using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace simulation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Canvas AppCanvas;
        public MainWindow()
        {
            InitializeComponent();
            AppCanvas = Layout;

            List<Field> EmptyLayout = new List<Field>();
            int x = 0, y = 0;
            for (int i=0;i<4;i++)
            {
                x = 0;
                for (int j=0;j<10;j++)
                {
                    EmptyLayout.Add(new EmptyField(x, y));
                    x += Field.SquareSize;
                }
                y += Field.SquareSize;
            }

            EmptyLayout.ForEach(e => e.Draw());




        }
    }
}

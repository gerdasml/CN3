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
using System.Windows.Threading;

namespace CN3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Graph _graph = new Graph();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        
        public MainWindow()
        {
            InitializeComponent();
            dispatcherTimer.Tick += (sender, args) => { _graph.Update(); };
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                HandleInput(inputBox.Text);
                //read what kind of command was typed in
                inputBox.Clear();
            }
        }

        private void HandleInput(string input)
        {

            if (string.IsNullOrWhiteSpace(input))
            {
                AddMessage("Invalid command");
                return;
            }
            string[] words = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            switch (words[0].ToLower())
            {
                case "add":
                    if (words.Length != 2)
                    {
                        AddMessage("Usage: add <name>");
                        return;
                    }
                    if (!_graph.AddNode(words[1]))
                    {
                        AddMessage("You've already tried this node.");
                    }
                    else AddMessage(string.Format("Node {0} added successfully.", words[1]));
                    break;
                case "remove_node":
                    if (words.Length != 2)
                    {
                        AddMessage("Usage: remove_node <name>");
                        return;
                    }
                    if (!_graph.RemoveNode(words[1]))
                    {
                        AddMessage("Such node does not exist.");
                    }
                    else AddMessage(string.Format("Node {0} successfully removed.", words[1]));
                    break;
                case "remove_edge":
                    if (words.Length != 3)
                    {
                        AddMessage("Usage: remove_edge <name1> <name2>");
                        return;
                    }
                    if (!_graph.RemoveEdge(words[1], words[2]))
                    {
                        AddMessage("Edge does not exist.");
                    }
                    else AddMessage(string.Format("Edge {0}-{1} successfully removed.", words[1], words[2]));
                    break;
                case "edge":
                    int w;
                    if (words.Length != 4 || !int.TryParse(words[3], out w))
                    {
                        AddMessage("Usage: edge <name1> <name2> <weight>");
                        return;
                    }
                    if (!_graph.AddEdge(words[1], words[2], w))
                    {
                        AddMessage("At least one node does not exist.");
                    }
                    else AddMessage(string.Format("Edge {0}-{1} successfully added.", words[1], words[2]));
                    break;
                case "edit":
                    int qw;
                    if (words.Length != 4 || !int.TryParse(words[3], out qw))
                    {
                        AddMessage("Usage: edit <name1> <name2> <weight>");
                        return;
                    }
                    if (!_graph.EditEdge(words[1], words[2], qw))
                    {
                        AddMessage("Edge does not exist.");
                    }
                    else AddMessage(string.Format("Edge {0}-{1} successfully edited.", words[1], words[2]));
                    break;
                case "send":
                    if (words.Length != 3)
                    {
                        AddMessage("Usage: send <name1> <name2>");
                        return;
                    }
                    var path = _graph.GetShortestPath(words[1], words[2]);
                    if (path == null)
                    {
                        AddMessage("Path does not exist.");
                    }
                    else AddMessage(string.Join("->", path.Select(x => x.Name)));
                    break;
                default:
                    AddMessage("Invalid command");
                    break;
            }
        }

        private void AddMessage(string message)
        {
            answersBox.AppendText(String.Format("{0}\n> ", message));
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cryptanalysis
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text = "Криптоанализ шифра перестановки";
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.SupportMultiDottedExtensions = true;
            openFileDialog.Title = "Выберите текстовый файл для загрузки";
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;
            using (StreamReader streamReader = new StreamReader(openFileDialog.FileName, Encoding.UTF8))
            {
                string[] array = new string[0];
                while (!streamReader.EndOfStream)
                {
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = streamReader.ReadLine();
                }
                this.textBox2.Lines = array;
            }
        }
        public string sort(string str)
        {
            char[] ss = str.ToCharArray();
            Array.Sort(ss);
            string s = "";
            for(int i = 0;i<ss.Length;i++)
            {
                s += ss[i];
            }
            return s;
        }
        public int[] position(string s,string sorted)
        {
            int[] a = new int[s.Length];
            for(int i = 0; i<s.Length;i++)
            {
                for(int j = 0;j<sorted.Length;j++)
                {
                    if(sorted[j]==s[i])
                    {
                        string s3 = "";
                        for(int k = 0;k<sorted.Length;k++)
                        {
                            if(k!=j)
                            {
                                s3 += sorted[k];
                            }
                            else
                            {
                                s3 += '~';
                            }
                        }
                        a[i] = j;
                        sorted = s3;
                        break;
                    }
                }
            }
            return a;
        }
        public int minind(int[] a)
        {
            int minn = a[0];
            int ind = 0;
            for(int i = 0;i<a.Length;i++)
            {
                if(a[i]<minn)
                {
                    minn = a[i];
                    ind = i;
                }
            }
            return ind;
        }
        public int maxx(int[] a)
        {
            int maxx = a[0];
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > maxx)
                {
                    maxx = a[i];
                }
            }
            return maxx;
        }
        public string block1(int[] s,string str)
        {
            string str2 = str;
            
            for (int i =0;i<str.Length;i++)
            {
                str2 = str2.Remove(s[i], 1).Insert(s[i], str[i].ToString());
              //  str2[s[i]] =str[i];
            }
            return str2;
        }
        public string blockmenshe(int[] s2,string str)
        {
            int[] s22 = new int[str.Length];
            for (int i = 0; i < s22.Length; i++)
            {
                s22[i] = s2[i];
            }
            int[] s23 = new int[str.Length];
            int ch = 0;
            int mx = maxx(s22);
            for (int i = 0; i < s23.Length; i++)
            {
                int ind = minind(s22);
                s23[ind] = ch;
                s22[ind] += mx + 1;
                ch += 1;
            }
            string res = "";
            for (int i = 0; i < s23.Length; i++)
            {
                res += str[s23[i]];
            }
            return res;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                string s1;
                int[] s2;
                if (textBox1.Text != "")
                {
                    s1 = sort(textBox1.Text);
                    s2 = position(textBox1.Text, s1);
                    string s3 = "";
                    for (int i = 0; i < s2.Length; i++)
                    {
                        if (i != s2.Length - 1)
                        {
                            s3 += Convert.ToString(s2[i]) + ",";
                        }
                        else
                        {
                            s3 += Convert.ToString(s2[i]);
                        }
                    }
                    label3.Text = "Ключ=" + s3;
                    if (textBox2.Text.Length <= s2.Length)
                    {
                        string res = blockmenshe(s2, textBox2.Text);
                        textBox3.Text = res;
                    }
                    else
                    {
                        int kol = (int)(textBox2.Text.Length / s2.Length);
                        string res = "";
                        string nach = textBox2.Text;
                        while (nach.Length % s2.Length != 0)
                        {
                            nach += " "; // дополнили нулями последний блок
                        }
                        for (int i = 0; i < nach.Length / s2.Length; i++)
                        {
                            string sss = nach.Substring(i * s2.Length, s2.Length);
                            res += blockmenshe(s2, sss);

                        }
                        textBox3.Text = res;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Парольная фраза не должна быть пустой."); return;
                }
            }
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.SupportMultiDottedExtensions = true;
            openFileDialog.Title = "Выберите текстовый файл для загрузки";
            if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                return;
            using (StreamReader streamReader = new StreamReader(openFileDialog.FileName, Encoding.UTF8))
            {
                string[] array = new string[0];
                while (!streamReader.EndOfStream)
                {
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = streamReader.ReadLine();
                }
                this.textBox3.Lines = array;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            
            if (textBox3.Text.Length != 0)
            {
                string s = textBox3.Text;
                int d = s.Length;
                var delitel = new List<int>();
                var set = new HashSet<int>();
                string res = "";

                for (int i = 2; i < Math.Sqrt(d) + 1; i++)
                {
                    if (d % i == 0 && !set.Contains(i))
                    {
                        set.Add(i);
                        delitel.Add(i);
                        if (d / i != i && !set.Contains(d / i))
                        {
                            delitel.Add(d / i);
                            set.Add(d / i);
                        }

                    }
                }
                if (!set.Contains(d))
                {
                    delitel.Add(d);  // длина ключа = длине текста
                }
                int[] mas = delitel.ToArray(); // содержит все делители для длины сообщения. (Возможные длины ключей.)
                Array.Sort(mas);
                if (textBox5.Text != "")
                {
                    try
                    {
                        int k = Convert.ToInt32(textBox5.Text);
                        mas = new int[1];
                        mas[0] = k;
                    }
                    catch(Exception)
                        {
                          mas = delitel.ToArray();
                        Array.Sort(mas);
                        }
                    }

                for(int i=0;i<mas.Length;i++)
                {
                    res += mas[i].ToString()+' ';
                }
                MessageBox.Show("Возможные длины ключей: "+res+" Сложность программы O(n!)");
                var zapret = new HashSet<string>() { "ёё", "ёщ", "ыё", "ёу", "йэ", "гъ", "кщ","щф", "щз", "эщ","щк",
                "гщ","щп","щт","щш","щг","щм","фщ","щл","щд","дщ","ьэ","чц","вй","ёц","ёэ","ёа", "йа","шя","шы","ёе","йё","гю","хя",
                "йы", "ця", "гь", "сй","хю","хё","ёи","ёо","яё","ёя","ёь","ёэ","ъж","эё","ъд","цё","уь","щч","чй","шй","шз","ыф","жщ",
                "жш", "жц","ыъ","ыэ","ыю","ыь","жй", "ыы", "жъ","жы","ъш","пй","ъщ","зщ","ъч","ъц","ъу","ъф","ъх", "ъъ", "ъы", "ыо",
                "жя","зй","ъь","ъэ","ыа","нй","еь","цй","ьй","ьл","ьр","пъ","еы","еъ","ьа","шъ","ёы","ёъ","ът","щс","оь","къ",
                "оы","щх","щщ","щъ","щц","кй","оъ","цщ","лъ","мй","шщ","ць","цъ","щй","йь","ъг","иъ","ъб","ъв","ъи",
                "ъй","ъп","ър","ъс","ъо","ън","ък","ъл","ъм","иы","иь", "йу","щэ","йы","йъ","щы","щю","щя","ъа","мъ","йй",
                "йж","ьу","гй","эъ","уъ","аь","чъ","хй","тй","чщ","ръ","юъ","фъ","уы","аъ","юь","аы","юы","эь","эы","бй","яь",
                "ьы","ьь","ьъ","яъ","яы","хщ","дй","фй"," ,"," .",
                "aa", "bc", "bf", "bh", "bk", "bn", "bp", "bq", "bw", "bx", "bz", "cb", "cf", "cg", "cj", "cm", "cv", "cw", "dx", "dz", 
                    "fk", "fp", "fq", "fv", "fx", "fz", "gb", "gj", "gk", "gp", "gq", "gv", "gx", "gz", "hg", "hj", "hk", "hp", "hq", 
                    "hv", "hx", "hz", "iw", "jb", "jc", "jd", "jf", "jg", "jh", "jj", "jk", "jl", "jm", "jn", "jp", "jq", "jr", "js", 
                    "jt", "jv", "jw", "jx", "jy", "jz", "kk", "kq", "kv", "kx", "kz", "lj", "lq", "lx", "lz", "md", "mj", "mk", "mq", 
                    "mv", "mw", "mx", "mz", "pc", "pf", "pj", "pq", "pv", "px", "pz", "qa", "qb", "qc", "qd", "qe", "qf", "qg", "qh", 
                    "qi", "qj", "qk", "ql", "qm", "qn", "qo", "qp", "qq", "qr", "qs", "qt", "qv", "qw", "qx", "qy", "qz", "rj", "rq", 
                    "rx", "sv", "sx", "sz", "tj", "tq", "tv", "tx", "uj", "uq", "uu", "uw", "vb", "vc", "vd", "vf", "vg", "vh", "vj", 
                    "vk", "vl", "vm", "vn", "vp", "vq", "vt", "vv", "vw", "vx", "vz", "wq", "wv", "wx", "wz", "xd", "xf", "xg", "xj", 
                    "xk", "xl", "xn", "xr", "xs", "xv", "xx", "xz", "yj", "yk", "yq", "yx", "zb", "zc", "zd", "zf", "zh", "zj", "zk", 
                    "zm", "zn", "zp", "zq", "zr", "zs", "zt", "zv", "zw", "zx", "q "};
                char ch,ch2;
               
                char[] mass1 = new char[32];
                char[] mass2 = new char[32];
                int n = 0;
                for (int i = 1072; i <= 1103; i++)
                {
                    ch = Convert.ToChar(i);
                    ch2 = Convert.ToChar(i - 32);
                    mass1[n] = ch;
                    mass2[n] = ch2;
                    n++;
                }

                for (int k = 0;k<32;k++)
                {
                    for(int j=0;j<32;j++)
                    {
                        string s7 = mass1[j].ToString() + mass2[k].ToString(); // выбрасываем сочетания малая буква+заглавная:  аБ бА и т.д.
                        zapret.Add(s7);
                    }
                }
                for (int i = 0; i < mas.Length; i++)
                {
                    MessageBox.Show("длина ключа = "+mas[i].ToString());
                    int strok = d / mas[i];
                    char[,] mm = new char[strok, mas[i]];
                    int[] m1 = new int[mas[i]];
                    int[] m2 = new int[m1.Length];
                    for (int t = 0; t < mas[i]; t++)
                    {
                        m1[t] = t;
                        m2[t] = t;
                    }
                    bool flag = false;
                    bool flag2 = Next(ref m2);
                    if(!flag2)
                    {
                        textBox4.Text = textBox3.Text;  // зашифрованное сообщение из одного символа..
                    }
                    HashSet<string> notuse = new HashSet<string>() { }; // ????
                                                                        //недопустимое множество для каждого нового разбиения будет другим т.к. допустим была строка: 0123456789,
                                                                        //первый раз разбили:   01234 и 56789 --> 0 1 (две части), допустим были запрещены 0 и 1, и 6 и 7, тогда при разбивке 01 23 45 67 89 - 
                    
                    while (flag2 && !flag)
                    {
                            bool fl2 = false;
                        string check = "";
                        
                      
                        int[] mu = new int[m1.Length];
                        for (int pp = 0; pp < m1.Length; pp++)
                        {
                            mu[m1[pp]] = pp;
                        }
                        for (int t = 0; t < m1.Length; t++)
                        {
                            check += mu[t].ToString();
                        }
                        foreach (string qq in notuse)
                              {
                                  if (check.Contains(qq))
                                  {
                                      fl2 = true;
                                      break;
                                  }
                              
                               }
                          
                            if (fl2)
                            {
                            flag2 = Next(ref m1);
                            continue;
                            }
                        int[] mss =m1;
                                string re5 = "";
                                string nach = textBox3.Text;
                                while (nach.Length % mss.Length != 0)
                                {
                                    nach += " "; // дополнили нулями последний блок
                                }


                        for (int l = 0; l < nach.Length / mss.Length; l++)
                                {
                                    string sss = nach.Substring(l * mss.Length, mss.Length);
                                    re5 += block1(mss, sss);
                                }
                        res = "";
                        
                        for (int j = 0; j < strok; j++)
                        {
                            for (int k = 0; k < mas[i]; k++)
                            {
                                mm[j, k] = re5[j * mas[i] + k];
                                res += mm[j, k] + " ";
                            }
                            res += "\n";
                        }
                        res = "";
                        for(int t = 0;t<m1.Length;t++)
                        {
                            res += m1[t].ToString()+" ";
                        }
                       
                        bool fl1 = false;
                            for (int t = 0; t < strok; t++)
                            {
                                    
                                for (int r = 0; r < m1.Length - 1; r++)
                                {
                                int k1 = r;
                                int k11 = r + 1;

                                string st = mm[t, k1].ToString() + mm[t, k11].ToString();
                                    if (zapret.Contains(st))
                                    {
                                         string notus = mu[k1].ToString() + mu[k11].ToString();
                                       
                                         notuse.Add(notus);
                                         fl1 = true;
                                        break;
                                    }
                                }
                                if (fl1)
                                {
                                    break;
                                }

                            }
                        if (!fl1)
                        {
                            textBox4.Text = re5;
                            string ms1 = "";
                            for (int u = 0; u < mss.Length; u++)
                            {
                                ms1 += mss[u].ToString() + ", ";
                            }
                            label8.Text = "Ключ= " + ms1;
                            var p = MessageBox.Show("Продолжить криптоанализ?", "", MessageBoxButtons.OKCancel);
                            if (p == DialogResult.OK)
                            {
                            }
                            else
                            {
                                flag = true;
                                break;
                            }
                        }
                        flag2 = Next(ref m1);
                   }
                    if (flag)
                     {
                         break;
                     }
                }
                
            }

        }
        bool Next(ref int[] arr)
        {
            int k, j, l;
            for (j = arr.Length - 2; (j >= 0) && (arr[j] >= arr[j + 1]); j--) { }
            if (j == -1)
            {
                arr = arr.OrderBy(c => c).ToArray();
                return false;
            }
            for (l = arr.Length - 1; (arr[j] >= arr[l]) && (l >= 0); l--) { }
            var tmp = arr[j];
            arr[j] = arr[l];
            arr[l] = tmp;
            for (k = j + 1, l = arr.Length - 1; k < l; k++, l--)
            {
                tmp = arr[k];
                arr[k] = arr[l];
                arr[l] = tmp;
            }
            return true;
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

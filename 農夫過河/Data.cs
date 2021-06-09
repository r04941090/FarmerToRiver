using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace 農夫過河
{
    class backDatas
    {
        public List<string> back_left { set; get; }
        public List<string> back_right { set; get; }
    }
    class Data
    {
        public List<string> list_Left { set; get; }
        public List<string> list_Right { set; get; }
        private List<backDatas> backDatas;
        private bool Direction;

        public Data()
        {
            string[] raw = new[]{ "農夫", "菜", "狼", "羊"};
            list_Left = new List<string>(raw);
            list_Right = new List<string>();
            backDatas = new List<backDatas> { new backDatas() { back_left = new List<string>(list_Left.ToArray()),
                                                                back_right= new List<string>()} };
            // 後面直接存list_Left、list_Right，因為共用同一個記憶體空間所以後續存的資料都會一樣
            Direction = true;
        }
        public bool Move(string item)
        {
            //  三元條件運算子
            List<string> AddList = (Direction ? list_Right : list_Left);
            List<string> RemoveList = (Direction ? list_Left : list_Right);

            if (RemoveList.Contains("農夫"))
            {
                if (item != "農夫")
                {
                    RemoveList.Remove(item);
                    AddList.Add(item);
                }
                RemoveList.Remove("農夫");
                AddList.Add("農夫");
                return true;
                // 移動成功
            }
            else
            {
                return false;
                // 移動失敗
            }
        }
        public string Check()
        {
            List<string> Checklist;

            if (list_Left.Count != 0)
            //  三元條件運算子
            // https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/operators/conditional-operator
            {
                Checklist = (!list_Left.Any((x) => x == "農夫") ? list_Left : list_Right);
                return (((Checklist.Any((x) => x == "羊") && Checklist.Any((x) => x == "菜")) ||
                         (Checklist.Any((x) => x == "羊") && Checklist.Any((x) => x == "狼"))) ? "Fail" : "Pass");
            }
            else
            {
                return "Finish";
            }
        }
        public bool Back()
        {
            if (backDatas.Count > 1)
            {
                list_Left = new List<string>(backDatas[backDatas.Count - 2].back_left);
                list_Right = new List<string>(backDatas[backDatas.Count - 2].back_right);
                backDatas.RemoveAt(backDatas.Count - 1);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddBackData()
        {
            backDatas.Add(new backDatas(){back_left = new List<string>(list_Left.ToArray()),
                                          back_right = new List<string>(list_Right.ToArray())});
            // 建構式：list<T>(Array) 
        }
        public void ChangeDirection(bool Direction)
        {
            this.Direction = Direction;
        }

    }
}

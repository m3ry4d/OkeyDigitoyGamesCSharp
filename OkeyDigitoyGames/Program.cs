using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkeyDigitoyGames
{
    static class Program
    {
        static Random rnd = new Random();
        static List<int> stones;
        static List<Stone> user1 = new List<Stone>();
        static List<Stone> user2 = new List<Stone>();
        static List<Stone> user3 = new List<Stone>();
        static List<Stone> user4 = new List<Stone>();
        static int okey;
        static int gosterge;
        static void Main(string[] args)
        {
            CreateStones();
            MixStones();
            GenerateOkey();
            DistributeStones();
            int bestHand = FindBestHand();
            Console.WriteLine("En iyi ele sahip oyuncu : user" + bestHand);
            Console.Read();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns> En iyi elin kac numarali oyuncuya ait oldugunu dondurur</returns>
        private static int FindBestHand()
        {
            int user1point = CalculateHandPoint(user1);
            int user2point = CalculateHandPoint(user2);
            int user3point = CalculateHandPoint(user3);
            int user4point = CalculateHandPoint(user4);
            if (user1point > user2point)
            {
                if (user3point > user4point)
                {
                    if (user3point > user1point)
                    {
                        return 3;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (user4point > user1point)
                    {
                        return 4;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            else
            {
                if (user3point > user4point)
                {
                    if (user3point > user2point)
                    {
                        return 3;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    if (user4point > user2point)
                    {
                        return 4;
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userStones">Oyuncunun elindeki taslari temsil eder</param>
        /// <returns>Algoritmaya bagli olarak elin bitmeye ne kadar yakin olduguna dair bir puani dondurur</returns>
        private static int CalculateHandPoint(List<Stone> userStones)
        {
            int point = 0;
            int jokerCount = userStones.Where(s => s.Number.Equals(okey)).Count();
            point += jokerCount * 3;
            userStones.Where(w => w.Number.Equals(okey)).ToList().ForEach(s => s.Number = 999);
            userStones.Where(w => w.Number.Equals(52)).ToList().ForEach(s => s.Number = okey);
            userStones.Sort(new SortByNumber());
            var groupsYellow = userStones.Where(x => ((int)(x.Number / 13)) == 0).GroupConsecutive();
            var groupsBlue = userStones.Where(x => ((int)(x.Number / 13)) == 1).GroupConsecutive();
            var groupsBlack = userStones.Where(x => ((int)(x.Number / 13)) == 2).GroupConsecutive();
            var groupsRed = userStones.Where(x => ((int)(x.Number / 13)) == 3).GroupConsecutive();

            for (int i = 0; i < groupsYellow.Count(); i++)
            {
                int plusPoint = GetPoint(groupsYellow.ToList()[i].Count());
                if (groupsYellow.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsYellow.ToList()[i])
                    {
                        userStones.Where(x => x.Number == item).First().Used = true;
                    }
                }
                point += plusPoint;
            }
            for (int i = 0; i < groupsBlue.Count(); i++)
            {
                int plusPoint = GetPoint(groupsBlue.ToList()[i].Count());
                if (groupsBlue.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsBlue.ToList()[i])
                    {
                        userStones.Where(x => x.Number == item).First().Used = true;
                    }
                }
                point += plusPoint;
            }
            for (int i = 0; i < groupsBlack.Count(); i++)
            {
                int plusPoint = GetPoint(groupsBlack.ToList()[i].Count());
                if (groupsBlack.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsBlack.ToList()[i])
                    {
                        userStones.Where(x => x.Number == item).First().Used = true;
                    }
                }
                point += plusPoint;
            }
            for (int i = 0; i < groupsRed.Count(); i++)
            {
                int plusPoint = GetPoint(groupsRed.ToList()[i].Count());
                if (groupsRed.ToList()[i].Count() > 2)
                {
                    foreach (var item in groupsRed.ToList()[i])
                    {
                        userStones.Where(x => x.Number == item).First().Used = true;
                    }
                }
                point += plusPoint;

            }
            for (int i = 0; i < userStones.Count; i++)
            {
                if (!userStones[i].Used)
                {
                    var lst = userStones.Where(x => !x.Used && (x.Number == userStones[i].Number + 13
                                    || x.Number == userStones[i].Number + 26
                                    || x.Number == userStones[i].Number + 39)
                     );
                    int count = lst.Count() + 1;
                    if (count == 2)
                    {
                        point += 1;
                    }
                    else if (count == 3)
                    {
                        point += 3;
                    }
                    else if (count == 4)
                    {
                        point += 4;
                    }
                    if (count > 2)
                    {
                        foreach (var item in lst)
                        {
                            userStones.Where(x => x.Number == item.Number).First().Used = true;
                        }
                        userStones.Where(x => x.Number == userStones[i].Number).First().Used = true;

                    }
                }
            }
            

            int twinPoint = 0;
            for (int i = 0; i < userStones.Count; i++)
            {
                if (userStones[i].Number != 999 && userStones.Where(x => x.Number == userStones[i].Number).Count() == 2)
                {
                    twinPoint += 2;
                }
            }
            twinPoint += (jokerCount * 2);



            for (int i = 0; i < userStones.Count; i++)
            {
                if (userStones[i].Number < 13)
                {
                    Console.WriteLine(userStones[i].Number % 13 + 1 + " sari");
                }
                else if (userStones[i].Number < 26)
                {
                    Console.WriteLine(userStones[i].Number % 13 + 1 + " mavi");

                }
                else if (userStones[i].Number < 39)
                {
                    Console.WriteLine(userStones[i].Number % 13 + 1 + " siyah");

                }
                else if (userStones[i].Number < 52)
                {
                    Console.WriteLine(userStones[i].Number % 13 + 1 + " kirmizi");

                }
            }
            Console.WriteLine(jokerCount + " Adet Okey");
            Console.WriteLine(Math.Max(point, twinPoint) + " Puan");
            Console.WriteLine("*******************************");

            return Math.Max(point, twinPoint);
        }
        /// <summary>
        /// Taslardaki tutuk sayisina gore puan dagitma islemi
        /// </summary>
        /// <param name="v">Sirali sayisi</param>
        /// <returns> Sirali sayiya gore puan dondurur</returns>
        private static int GetPoint(int v)
        {
            int newPoint = 0;
            switch (v)
            {
                case 0:
                case 1:
                    break;
                case 2:
                    newPoint = 1;
                    break;
                case 3:
                case 4:
                    newPoint = v;
                    break;
                case 5:
                    newPoint = 4;
                    break;
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    newPoint = v;
                    break;
                default:
                    break;
            }

            return newPoint;
        }
        /// <summary>
        /// Taslari oyunculara dagitir
        /// </summary>
        private static void DistributeStones()
        {
            int gostergeGivenNum = 0;
            for (int i = 0; i < 56; i++)
            {
                int stoneToAdd = stones[i];

                #region Gosterge 2 kere dagitilmasin kontrolu
                if (gosterge == stones[i])
                {
                    gostergeGivenNum++;
                    if (gostergeGivenNum == 2)
                    {
                        stoneToAdd = stones[57];
                    }
                }
                #endregion

                if (i < 14)
                {
                    user1.Add(new Stone() { Number = stoneToAdd });
                }
                else if (i < 28)
                {
                    user2.Add(new Stone() { Number = stoneToAdd });
                }
                else if (i < 42)
                {
                    user3.Add(new Stone() { Number = stoneToAdd });
                }
                else if (i < 56)
                {
                    user4.Add(new Stone() { Number = stoneToAdd });
                }
            }



            int newStone = gostergeGivenNum == 2 ? stones[58] : stones[57]; /// Fazladan tas verilirken ikinci kez gosterge verilmesin kontrolu 
            if (gostergeGivenNum != 0 && newStone == gosterge)
            {
                newStone = stones[59];
            }
            int fifteenStoneUser = rnd.Next(0, 4);
            switch (fifteenStoneUser)
            {
                case 0:
                    user1.Add(new Stone() { Number = newStone });
                    break;
                case 1:
                    user2.Add(new Stone() { Number = newStone });
                    break;
                case 2:
                    user3.Add(new Stone() { Number = newStone });
                    break;
                case 3:
                    user4.Add(new Stone() { Number = newStone });
                    break;
                default:
                    break;
            }

        }
        /// <summary>
        /// Rastgele bir tasi okey olarak belirler
        /// </summary>
        private static void GenerateOkey()
        {
            gosterge = rnd.Next(0, 53);
            switch (gosterge)
            {
                case 12:
                    okey = 0;
                    break;
                case 25:
                    okey = 13;
                    break;
                case 38:
                    okey = 26;
                    break;
                case 51:
                    okey = 39;
                    break;
                default:
                    okey = gosterge + 1;
                    break;
            }

        }
        /// <summary>
        /// Taslari karistirir.
        /// </summary>
        private static void MixStones()
        {
            var result = stones.OrderBy(item => rnd.Next());
            stones = result.ToList();
        }
        /// <summary>
        ///  Taslarin oldugu Array'i hazirlar.
        /// </summary>
        private static void CreateStones()
        {
            stones = new List<int>();
            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 53; i++)
                {
                    stones.Add(i);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">Oyuncunun elindeki ayni renkteki tas listesi</param>
        /// <returns>Ayni renkten olan taslari seri olarak dondurur. Kac tutuk oldugu bu serilerin uzunlugundan anlasilir.</returns>
        public static IEnumerable<IEnumerable<int>> GroupConsecutive(this IEnumerable<Stone> list)
        {
            var group = new List<int>();
            foreach (var i in list)
            {
                if (group.Count == 0 || i.Number - group[group.Count - 1] <= 1)  /// Bir oncekiyle ayniysa ya da ardisiksa gruba ekle
                {
                    if (!group.Contains(i.Number))                              //Ayni tastan 2 kere varsa gruba ekleme
                        group.Add(i.Number);
                }
                else if (i.Number % 13 == 12 && group.Count > 1 && list.Where(x => x.Number == 0).Count() > 0) /// 12 , 13 varsa 1 de gelebilir
                {
                    group.Add(i.Number);
                }
                else                                                              /// Ardisik olmadigi icin yeni grup olustur
                {
                    yield return group;
                    group = new List<int> { i.Number };
                }
            }
            yield return group;
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_WPF
{
    internal class GameLogic
    {
        /// <summary>
        /// Принимает на вход количество дайсов и размерность (к6, к12, к20, к100 и т.д)
        /// </summary>
        /// <param name="diceCount"></param>
        /// <param name="dice"></param>
        /// <returns></returns>
        public static int Dice(int diceCount, int dice)
        {
            Random roll = new Random();

            int sum = 0; //Сумма значений с бросков

            for (int i = 0; i < diceCount; i++)
            {

                sum += (int)roll.NextInt64(1, dice + 1);

            }
            return sum;
        }

        /// <summary>
        /// <para>
        /// Возвращает модификатор характеристики в зависимости от её значения 
        /// </para>
        /// <para>
        /// <see href="https://dnd.su/articles/mechanics/468-using_ability_scores/">
        /// 
        /// Подробнее
        /// </see>
        /// </para>
        /// </summary>
        /// 
        /// <param name="skill"></param>
        /// <returns></returns>
        public static int Modifier(int skill)
        {
            int mod = (skill <= 1) ? -5 : (skill == 2 || skill == 3) ? -4 : //Тернарные операторы друг в друге
            (skill == 4 || skill == 5) ? -3 : (skill == 6 || skill == 7) ? -2 :
            (skill == 8 || skill == 9) ? -1 : (skill == 10 || skill == 11) ? 0 :
            (skill == 12 || skill == 13) ? 1 : (skill == 14 || skill == 15) ? 2 :
            (skill == 16 || skill == 17) ? 3 : (skill == 18 || skill == 19) ? 4 :
            (skill == 20 || skill == 21) ? 5 : (skill == 22 || skill == 23) ? 6 :
            (skill == 24 || skill == 25) ? 7 : (skill == 26 || skill == 27) ? 8 :
            (skill == 28 || skill == 29) ? 9 : (skill == 30) ? 10 : -258419;
            return mod;
        }
    }
}

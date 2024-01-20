using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DND_WPF
{
    internal class GameLogic
    {
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
        public static bool? BattleSteps(Dictionary<string, int> chars, Dictionary<string, int> enemies)
        {
            int C_hp = chars["HP"];
            int C_dmg = chars["damage"];
            int C_def = chars["defence"];
            int C_mod = chars["mod"];
            int E_hp = enemies["HP"];
            int E_dmg = enemies["damage"];
            int E_def = enemies["defence"];
            int E_mod = enemies["mod"];

            while (C_hp > 0 && E_hp  > 0)
            {
                int C_step = Dice(1, 20);
                if (C_step + C_mod < E_def)
                {

                }
                else if (C_step + C_mod > E_def && C_step < 20)
                {
                    E_hp -= C_dmg;
                }
                else if (C_step == 20)
                {
                    E_hp -= C_dmg; E_hp -= C_dmg - C_mod;
                }

                int E_step = Dice(1, 20);
                if (E_step + E_mod < C_def)
                {

                }
                else if (E_step + E_mod > C_def && E_step < 20)
                {
                    C_hp -= E_dmg;
                }
                else if (E_step == 20)
                {
                    C_hp -= E_dmg; C_hp -= E_dmg - E_mod;
                }
            }
            if (E_hp <= 0) return true;
            else if (C_hp <= 0) return false;
            else return null;


        }
    }
}

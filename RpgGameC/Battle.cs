namespace RpgGameC

{

    internal static class Battle
    {
        public static BattleRes RunElite(Map map, RunState run)
        {
            return Run(run,new BattleCfg

                {
                    EnemyMaxHp = Balance.EliteHp(map.currentMapNumber),
                    EnemyDamagePerHit = Balance.EliteAtk(map.currentMapNumber),
                    EnemyIdleFrames = EnemyArt.EnemyIdleFrames,
                    EnemyColor = ConsoleColor.Red,
                    EnemyAttackMessage = "정예 몬스터가 {0} 데미지를 입혔다!",
                    VictoryGoldReward = 0,
                });
        }

        public static BattleRes Run(RunState run, BattleCfg config)
        {
            int enemyHp = config.EnemyMaxHp;

            int enemyDamagePerHit = config.EnemyDamagePerHit;

            string[][] enemyFrames = config.EnemyIdleFrames;

            ConsoleColor enemyColor = config.EnemyColor;



            Random rand = new Random();



            while (true)

            {

                int action = BattleIn.WaitForBattleActionSelection(

                    run,

                    enemyHp,

                    enemyFrames,

                    enemyColor);



                int playerDamage = 0;

                bool isPotionUsed = false;

                bool useHeavyLunge = false;



                if (action == 0)

                {

                    playerDamage = Balance.PlayerNormalAttackDamage * run.AtkMult();

                }

                else if (action == 1)

                {

                    int successPercent = run.SplRate();

                    bool specialHit = rand.Next(1, 101) <= successPercent;



                    if (!specialHit)

                    {

                        playerDamage = 0;



                        BattleFx.PlayPlayerStagger(

                            run.PlayerHp,

                            enemyHp,

                            run.PotionCount,

                            enemyFrames,

                            enemyColor);



                        Console.Clear();

                        BattleUi.DrawBattleScreen(

                            run.PlayerHp,

                            enemyHp,

                            run.PotionCount,

                            true,

                            enemyIdleFrame: 0,

                            enemySpriteOverride: null,

                            enemyColor: enemyColor,

                            enemyIdleFrames: enemyFrames);

                        BattleUi.DrawBattleActionMenu(0, run.SplRate());



                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight - 4);

                        Console.WriteLine("공격 실패!");



                        Thread.Sleep(1000);

                    }

                    else

                    {

                        playerDamage = Balance.PlayerSpecialAttackDamage * run.AtkMult();

                        useHeavyLunge = true;

                    }

                }

                else if (action == 3)

                {

                    Console.Clear();



                    Console.ForegroundColor = ConsoleColor.Cyan;

                    Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);

                    Console.WriteLine("전장에서 도망쳤다!");

                    Console.ResetColor();



                    Thread.Sleep(900);

                    return BattleRes.Flee;

                }

                else if (action == 2)

                {

                    if (run.PotionCount > 0)

                    {

                        int maxHp = run.MaxHp();

                        int potionHeal = Balance.PotHeal(maxHp);

                        run.PlayerHp += potionHeal;



                        if (run.PlayerHp > maxHp)

                        {

                            run.PlayerHp = maxHp;

                        }



                        run.PotionCount--;

                        isPotionUsed = true;



                        Console.Clear();

                        BattleUi.DrawBattleScreen(

                            run.PlayerHp,

                            enemyHp,

                            run.PotionCount,

                            true,

                            enemyIdleFrame: 0,

                            enemyColor: enemyColor,

                            enemyIdleFrames: enemyFrames);

                        BattleUi.DrawBattleActionMenu(0, run.SplRate());



                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight - 4);

                        Console.WriteLine($"체력 회복! [+{potionHeal}] 남은 포션 : {run.PotionCount}");



                        Thread.Sleep(1000);

                    }

                    else

                    {

                        Console.Clear();

                        BattleUi.DrawBattleScreen(

                            run.PlayerHp,

                            enemyHp,

                            run.PotionCount,

                            true,

                            enemyIdleFrame: 0,

                            enemyColor: enemyColor,

                            enemyIdleFrames: enemyFrames);

                        BattleUi.DrawBattleActionMenu(0, run.SplRate());



                        Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight - 4);

                        Console.WriteLine("포션이 없습니다!");



                        Thread.Sleep(1000);

                        continue;

                    }

                }



                if (isPotionUsed == false)

                {

                    if (playerDamage > 0)

                    {

                        BattleFx.PlayPlayerAttack(

                            run.PlayerHp,

                            enemyHp,

                            run.PotionCount,

                            useHeavyLunge,

                            enemyFrames,

                            enemyColor);

                    }



                    enemyHp -= playerDamage;



                    if (enemyHp < 0)

                    {

                        enemyHp = 0;

                    }



                    Console.Clear();

                    BattleUi.DrawBattleScreen(

                        run.PlayerHp,

                        enemyHp,

                        run.PotionCount,

                        true,

                        enemyIdleFrame: 0,

                        enemyColor: enemyColor,

                        enemyIdleFrames: enemyFrames);

                    BattleUi.DrawBattleActionMenu(0, run.SplRate());



                    Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight - 4);

                    Console.WriteLine($"플레이어가 {playerDamage} 데미지를 입혔다!");



                    Thread.Sleep(1000);



                    if (enemyHp <= 0)

                    {

                        if (config.VictoryGoldReward > 0)

                        {

                            run.Gold += config.VictoryGoldReward;

                        }



                        Console.Clear();



                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2);



                        if (config.VictoryGoldReward > 0)

                        {

                            Console.WriteLine($"전투 승리! Gold +{config.VictoryGoldReward}");

                        }

                        else

                        {

                            Console.WriteLine("전투에서 승리!");

                        }



                        Console.ResetColor();



                        Thread.Sleep(1500);

                        return BattleRes.Win;

                    }

                }



                BattleFx.PlayEnemyStrike(

                    run.PlayerHp,

                    enemyHp,

                    run.PotionCount,

                    enemyFrames,

                    enemyColor);



                int damageTaken = Math.Max(1, enemyDamagePerHit);

                run.PlayerHp -= damageTaken;



                if (run.PlayerHp < 0)

                {

                    run.PlayerHp = 0;

                }



                Console.Clear();

                BattleUi.DrawBattleScreen(

                    run.PlayerHp,

                    enemyHp,

                    run.PotionCount,

                    true,

                    enemyIdleFrame: 0,

                    enemyColor: enemyColor,

                    enemyIdleFrames: enemyFrames);

                BattleUi.DrawBattleActionMenu(0, run.SplRate());



                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight - 4);

                Console.WriteLine(string.Format(config.EnemyAttackMessage, damageTaken));



                Thread.Sleep(1500);



                if (run.PlayerHp <= 0)

                {

                    Console.Clear();



                    Console.ForegroundColor = ConsoleColor.Red;

                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);

                    Console.WriteLine("전투 패배...");

                    Console.ResetColor();



                    Thread.Sleep(900);

                    return BattleRes.Lose;

                }

            }

        }

    }

}


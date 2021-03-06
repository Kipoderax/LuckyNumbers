import { UserSendedBets } from './userSendedBets';

export interface LottoResult {
    failGoal: number;
    goal1Number: number;
    goal2Numbers: number;
    goal3Numbers: number;
    goal4Numbers: number;
    goal5Numbers: number;
    goal6Numbers: number;
    totalCostBets: number;
    totalEarnMoney: number;
    totalEarnExp: number;
    betsWithGoal3Numbers: UserSendedBets[];
    betsWithGoal4Numbers: UserSendedBets[];
    betsWithGoal5Numbers: UserSendedBets[];
    betsWithGoal6Numbers: UserSendedBets[];
}
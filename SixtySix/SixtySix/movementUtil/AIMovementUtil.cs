﻿using SixtySix.enums;
using SixtySix.RuleBasedEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixtySix
{
    public class AIMovementUtil
    {
        public static Card MakeTurn(Player player, Deck deck, Card playedFromOther=null)
        {
            Card card = null;
            if (player.PlayStrategy == PlayStrategy.MCTS)
            {
                card = GiveMCTSBasedCard(player, deck, playedFromOther);
            }
            else if (player.PlayStrategy == PlayStrategy.RuleBased)
            {
                card = RuleBasedMovementUtil.GiveRuleBasedCard(player, deck, playedFromOther);
            }
            else if (player.PlayStrategy == PlayStrategy.Random)
            {
                card = GiveRandomBasedCard(player, deck, playedFromOther);
            }
            
            Console.WriteLine("AI Hand: " + player.ToStringPlayerCards());
            Console.WriteLine("AI has played: {0}", card);
            player.GiveCard(card);

            //check for swapping opened card and swap if is allowed
            if (SixtySixUtil.CanSwap(player.Cards, deck))
            {
                SixtySixUtil.SwapOpenedCard(player, deck);
            }

            //Check for additional point -> (20 or 40)
            //TODO Idea for modification: Player choose if he wants to call his announce. If has more than one announce can choose which one wants to play.
            if (SixtySixUtil.HasForty(player.Cards, card, deck))
            {
                SixtySixUtil.CallForty(player);
            }
            else if (SixtySixUtil.HasTwenty(player.Cards, card, deck))
            {
                SixtySixUtil.CallTwenty(player);
            }

            return card;
        }

        private static Card GiveRandomBasedCard(Player player, Deck deck, Card playedFromOther = null)
        {
            var rand = new Random();
            var card = player.Cards[rand.Next(0, player.Cards.Count)];

            return card;
        }

        

        private static Card GiveMCTSBasedCard(Player player, Deck deck, Card playedFromOther = null)
        {
            return null;
        }

        public static int GetDeckSplittingIndex()
        {
            Random rand = new Random(System.DateTime.Now.Millisecond);
            var splitIndex = rand.Next(0, Constants.DECK_COUNT);

            return splitIndex;
        }
    }
}

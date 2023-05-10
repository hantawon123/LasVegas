using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System;
using System.Collections;

public class MLPlayer : MonoBehaviour
{
    public NNModel modelAsset;
    private Model model;
    private IWorker engine;
    private Tensor inputTensor = new Tensor(new int[] { 1, 95 });


    // Singleton
    public static MLPlayer instance;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        model = ModelLoader.Load(modelAsset);
        engine = WorkerFactory.CreateWorker(WorkerFactory.Type.CSharpBurst, model);
    }

    private void PrepareInput(int playerIndex)
    {
        /*
        Input Size
        Game 1 * (NumPlayer 1 + Round 1) = 2
        Player 1 * (Index 5 + Money 1 + DiceEyeNum 6 + WhiteDiceEyeNum 6) = 18
        Casinos 6 * (Banknotes 5 + PlayerDiceNum 5) = 60
        OtherPlayers 5 * (Money 1 + DiceNum 1 + WhiteDiceNum 1) = 15
        SUM = 95
        */

        // Game Info (0 ~ 1)
        inputTensor[0, 0] = 1.0f;  // Num Player (X / 5)
        inputTensor[0, 1] = (float)GameManager.instance.round / 4;  // Round (X / 4)

        // Player Info (2 ~ 19)
        Player player = PlayerManager.instance.PlayerList[playerIndex - 1].GetComponent<Player>();
        // List<int> diceList = CasinoPlayer.instance.GetDiceList();
        List<int> diceList = PlayerManager.instance.PMGetDiceList(playerIndex);

        inputTensor[0, 2] = 0.0f;
        inputTensor[0, 3] = 0.0f;
        inputTensor[0, 4] = 0.0f;
        inputTensor[0, 5] = 0.0f;
        inputTensor[0, 6] = 0.0f;
        inputTensor[0, playerIndex + 1] = 1.0f;  // Player index

        inputTensor[0, 7] = (float)player.sum / 1000000;  // Money (X / 1,000,000)
        for (int i = 0; i < 6; i++)   // Num Dice of Each Eyes (X / 8)
        {
            inputTensor[0, 8 + i] = (float)diceList[i] / 8;
        }
        for (int i = 0; i < 6; i++)  // Num White Dice of Each Eyes (X / 4)
        {
            inputTensor[0, 14 + i] = 0.0f;
        }

        // Iter 6 Casonos (20 ~ 79)
        int casinoTensorIndex = 20;
        for (int casinoIndex = 0; casinoIndex < 6; casinoIndex++)
        {
            Casino casino = CasinoManager.instance.casinoList[casinoIndex].GetComponent<Casino>();
            
            // Up to 5 Banknotes (X / 100,000)
            for (int bnIndex = 0; bnIndex < 5; bnIndex++)
            {
                List<int> tempCasinoMoneyList = casino.casinoMoneyList.ToList();  // DeepCopy (We have to check it again)
                tempCasinoMoneyList.Sort();
                tempCasinoMoneyList.Reverse();
                if (bnIndex < tempCasinoMoneyList.Count) {
                    inputTensor[0, casinoTensorIndex + bnIndex] = (float)tempCasinoMoneyList[bnIndex] / 100000;
                } else {
                    inputTensor[0, casinoTensorIndex + bnIndex] = 0.0f;
                }
            }

            // Dice Num of Each Players (X / 8)
            inputTensor[0, casinoTensorIndex + 5] = casino.player1dc / 8;
            inputTensor[0, casinoTensorIndex + 6] = casino.player2dc / 8;
            inputTensor[0, casinoTensorIndex + 7] = casino.player3dc / 8;
            inputTensor[0, casinoTensorIndex + 8] = casino.player4dc / 8;
            inputTensor[0, casinoTensorIndex + 9] = casino.player5dc / 8;

            casinoTensorIndex += 10;
        }

        // Iter Other 5 Players (80 ~ 94)
        int otherPlayerTensorIndex = 80;
        for (int otherPlayerIndex = 0; otherPlayerIndex < 5; otherPlayerIndex++)
        {
            Player otherPlayer = PlayerManager.instance.PlayerList[otherPlayerIndex].GetComponent<Player>();
            inputTensor[0, otherPlayerTensorIndex + 0] = (float)otherPlayer.sum / 1000000;  // Money (X / 1,000,000)
            inputTensor[0, otherPlayerTensorIndex + 1] = (float)otherPlayer.numsofDice / 8;  // NumDice (X / 8)
            inputTensor[0, otherPlayerTensorIndex + 2] = 0.0f;  // NumWhiteDice (X / 4)
            otherPlayerTensorIndex += 3;
        }
    }

    public int ExecuteML(int playerIndex)
    {
        // Set Input Tensor
        PrepareInput(playerIndex);

        // Execute
        engine.Execute(inputTensor);
        var output = engine.PeekOutput();

        // Get Results
        var argSorted = output.ArgSort();

        int res = 0;
        // List<int> diceList = CasinoPlayer.instance.GetDiceList();
        List<int> diceList = PlayerManager.instance.PMGetDiceList(playerIndex);
        for (int i = 0; i < 6; i++)
        {
            res = argSorted[0][5 - i];
            if (diceList[res] > 0) break;  // If player has selected dice
        }

        Resources.UnloadUnusedAssets();
        return res + 1;
    }

    private void OnDestroy()
    {
        engine.Dispose();
        inputTensor.Dispose();
    }
}
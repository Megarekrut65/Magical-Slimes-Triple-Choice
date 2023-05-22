using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DataManagement;
using Firebase.Extensions;
using Firebase.Firestore;
using Global;
using IncrementalMode;
using UnityEngine;

namespace FightingMode.Lobby.Rating
{
    public class RatingListLoader : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject itemList;

        [SerializeField] private Color background1;
        [SerializeField] private Color background2;
        
        private CollectionReference _colRef;
        private void Start()
        {
            FirebaseFirestore db = FirebaseManager.Fs;
            _colRef = db.Collection("users");
            OrderByCups();

        }

        public void OrderByCups()
        {
            ClearList();
            TakeListFromDatabase("cups");
        }
        public void OrderByLevel()
        {
            ClearList();
            TakeListFromDatabase("maxLevel");
        }
        private void ClearList()
        {
            foreach (Transform child in parent) {
                Destroy(child.gameObject);
            }
        }
        private void TakeListFromDatabase(string order)
        {
            _colRef.OrderByDescending(order)
                .Limit(25)
                .GetSnapshotAsync()
                .ContinueWithOnMainThread(LoadList);
        }

        private void LoadList( Task<QuerySnapshot> task)
        {
            if (task.IsFaulted)
            {
                CustomLogger.Log(task.Exception?.Message);
                return;
            }
            int index = 0;
            foreach (DocumentSnapshot data in task.Result)
            {
                Dictionary<string, object> dictionary = data.ToDictionary();
                Rating rating = new Rating
                {
                    username = dictionary["username"] as string,
                    cups = Convert.ToInt32(dictionary["cups"]),
                    maxLevel = Convert.ToInt32(dictionary["maxLevel"]),
                    maxEnergy = new Energy(
                        BigInteger.Parse(dictionary["maxEnergy"] as string ?? "0")).ToString()
                };
                CreateItem(index++, rating);
            }
            
        }
        private void CreateItem(int index, Rating rating)
        {
            GameObject obj = Instantiate(itemList, parent, false);
            RatingItem item = obj.GetComponent<RatingItem>();
            item.SetRating(rating, index % 2 == 0?background1:background2);
        }
    }
}
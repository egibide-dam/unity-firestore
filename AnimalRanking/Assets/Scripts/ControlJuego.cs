using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Firestore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ControlJuego : MonoBehaviour
{
    private GameObject botonIzquierda;
    private GameObject botonDerecha;
    private GameObject marcador;

    private Random random = new Random();

    private int izquierda = 30;
    private int derecha = 40;

    private FirebaseFirestore db;

    private String[] imagenes =
    {
        "anteater", "bear", "beaver", "boar", "buffalo-1", "buffalo", "cat", "chicken", "cow", "crow", "dog-1", "dog",
        "donkey", "elephant", "fox", "giraffe", "hedgehog", "hen", "hippopotamus", "horse", "kangaroo", "koala",
        "leopard", "lion", "marten", "monkey-1", "monkey", "mouse", "octopus", "ostrich", "owl", "panda", "parrot",
        "penguin-1", "penguin", "pig", "polar-bear", "rabbit", "racoon", "rhinoceros", "rooster", "seagull", "seal",
        "sheep-1", "sheep", "sloth", "snake", "tiger", "whale", "zebra",
    };

    private String[] nombres =
    {
        "Anteater", "Bear", "Beaver", "Boar", "Buffalo", "Buffalo", "Cat", "Chicken", "Cow", "Crow", "Dog", "Dog",
        "Donkey", "Elephant", "Fox", "Giraffe", "Hedgehog", "Hen", "Hippopotamus", "Horse", "Kangaroo", "Koala",
        "Leopard", "Lion", "Marten", "Monkey", "Monkey", "Mouse", "Octopus", "Ostrich", "Owl", "Panda", "Parrot",
        "Penguin", "Penguin", "Pig", "Polar Bear", "Rabbit", "Racoon", "Rhinoceros", "Rooster", "Seagull", "Seal",
        "Sheep", "Sheep", "Sloth", "Snake", "Tiger", "Whale", "Zebra",
    };

    void Start()
    {
        // Conexión a Firestore
        db = FirebaseFirestore.GetInstance(FirebaseApp.DefaultInstance);

        botonIzquierda = GameObject.Find("BotonIzquierda");
        botonDerecha = GameObject.Find("BotonDerecha");
        marcador = GameObject.Find("Marcador");

        izquierda = random.Next(imagenes.Length);
        nuevaImagen(botonIzquierda, izquierda);
        derecha = random.Next(imagenes.Length);
        nuevaImagen(botonDerecha, derecha);

        // Crear los documentos vacíos
        foreach (var nombre in nombres)
        {
            DocumentReference docRef = db.Collection("animals").Document(nombre.ToLower());

            var datos = new Dictionary<string, object>
            {
            };

            docRef.SetAsync(datos, SetOptions.MergeAll);
        }

        // Observar una colección
        CollectionReference animalsRef = db.Collection("animals");
        Query query = animalsRef.OrderByDescending("count");

        ListenerRegistration listener = query.Listen(snapshot =>
        {
            String top10 = "Top 10\n\n";

            foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
            {
                Dictionary<string, object> animal = documentSnapshot.ToDictionary();
                top10 += $"{documentSnapshot.Id} ({animal["count"]})\n";
            }

            marcador.GetComponent<TMP_Text>().text = top10;
        });
    }

    private void nuevaImagen(GameObject boton, int posicion)
    {
        boton.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>(imagenes[posicion]);
        boton.GetComponentInChildren<TMP_Text>().text = nombres[posicion];
    }

    private void actualizarMarcador(String texto)
    {
        marcador.GetComponent<TMP_Text>().text = texto;
    }

    public void botonIzquierdoPulsado()
    {
        DocumentReference docRef = db.Collection("animals").Document(nombres[izquierda].ToLower());
        docRef.UpdateAsync("count", FieldValue.Increment(1));

        izquierda = random.Next(imagenes.Length);
        nuevaImagen(botonIzquierda, izquierda);
    }

    public void botonDerechoPulsado()
    {
        DocumentReference docRef = db.Collection("animals").Document(nombres[derecha].ToLower());
        docRef.UpdateAsync("count", FieldValue.Increment(1));

        derecha = random.Next(imagenes.Length);
        nuevaImagen(botonDerecha, derecha);
    }
}
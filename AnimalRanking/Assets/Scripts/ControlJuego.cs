using System;
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
        botonIzquierda = GameObject.Find("BotonIzquierda");
        botonDerecha = GameObject.Find("BotonDerecha");
        marcador = GameObject.Find("Marcador");

        izquierda = random.Next(imagenes.Length);
        nuevaImagen(botonIzquierda, izquierda);
        derecha = random.Next(imagenes.Length);
        nuevaImagen(botonDerecha, derecha);
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
        actualizarMarcador(nombres[izquierda].ToLower());

        izquierda = random.Next(imagenes.Length);
        nuevaImagen(botonIzquierda, izquierda);
    }

    public void botonDerechoPulsado()
    {
        actualizarMarcador(nombres[derecha].ToLower());

        derecha = random.Next(imagenes.Length);
        nuevaImagen(botonDerecha, derecha);
    }
}
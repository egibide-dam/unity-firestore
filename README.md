# unity-firestore

Ejemplo sobre cómo conectar [Google Cloud Firestore](https://firebase.google.com/docs/firestore?hl=es-419) a un juego creado en Unity.

Iconos: [Animal Icon Pack](https://www.flaticon.com/packs/animals-3) por [Freepik](https://www.flaticon.com/authors/freepik).

## Bug

[Un problema](https://github.com/firebase/quickstart-unity/issues/638) con Firestore hace que el programa se cierre de forma inesperada.

Si ocurre, cambiad la línea de conexión por esta:

```csharp
// Conexión a Firestore
db = FirebaseFirestore.GetInstance(FirebaseApp.Create());
```

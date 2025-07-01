# proyecto-videojuego

Este repositorio contiene un ejemplo de juego de Ping Pong realizado con Unity.
Si solo quieres probarlo r\u00e1pidamente, abre `html/index.html` en tu navegador
y podr\u00e1s jugar inmediatamente sin instalar nada.

## Configuraci\u00f3n en Unity

1. Abre Unity y crea un proyecto 2D nuevo o usa uno existente.
2. Copia la carpeta `Assets` de este repositorio dentro del proyecto.
3. Crea dos objetos para las paletas y as\u00edgnales el script `PaddleController`.
4. Crea un objeto para la pelota con un `Rigidbody2D` y el script `Ball`.
5. A\u00f1ade dos objetos con colisionador en los extremos izquierdo y derecho, n\u00f3mbralos `LeftGoal` y `RightGoal` y dales la etiqueta `Goal`.
6. Crea dos elementos `UI Text` para mostrar las puntuaciones y enl\u00e1zalos en el `GameManager`.

Las paletas se mueven con `W/S` para el jugador izquierdo y `Flecha Arriba/Flecha Abajo` para el derecho. Al pulsar Play comenzar\u00e1 la partida.

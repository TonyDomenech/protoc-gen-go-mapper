# proyecto-videojuego

Este repositorio contiene un ejemplo de juego de Ping Pong realizado con Unity.
Si solo quieres probarlo r\u00e1pidamente, abre `html/index.html` en tu navegador
y podr\u00e1s jugar inmediatamente sin instalar nada. Aparecer\u00e1 un men\u00fa
inicial donde elegir si juegas contra otra persona en local o contra la m\u00e1quina.
Si eliges la m\u00e1quina dispondr\u00e1s de cinco dificultades: F\u00e1cil, Medio,
Avanzado, Dif\u00edcil e Imposible. El jugador 1 aparece en azul y el jugador 2 en rojo.
El campo de juego es m\u00e1s ancho para facilitar la reacci\u00f3n y, tras cada
punto, la pelota se queda dos segundos en el centro antes de ponerse en
marcha de nuevo.

## Configuraci\u00f3n en Unity

1. Abre Unity y crea un proyecto 2D nuevo o usa uno existente.
2. Copia la carpeta `Assets` de este repositorio dentro del proyecto.
3. Crea dos objetos para las paletas y as\u00edgnales el script `PaddleController`.
   La paleta izquierda (jugador 1) se muestra en azul y la derecha (jugador 2) en rojo.
4. Crea un objeto para la pelota con un `Rigidbody2D` y el script `Ball`.
5. A\u00f1ade dos objetos con colisionador en los extremos izquierdo y derecho, n\u00f3mbralos `LeftGoal` y `RightGoal` y dales la etiqueta `Goal`.
6. Crea dos elementos `UI Text` para mostrar las puntuaciones y enl\u00e1zalos en el `GameManager`.
7. Para jugar contra la m\u00e1quina a\u00f1ade el componente `AIPaddleController` a la pala derecha, referencia ese objeto en el `GameManager` y selecciona la dificultad (1 a 5).

Las paletas se mueven con `W/S` para el jugador izquierdo y `Flecha Arriba/Flecha Abajo` para el derecho. Al pulsar Play comenzar\u00e1 la partida.

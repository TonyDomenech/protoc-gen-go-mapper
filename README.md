# proyecto-videojuego

Este repositorio contiene un ejemplo de juego de Ping Pong realizado con Unity.
Si solo quieres probarlo r\u00e1pidamente, abre `html/index.html` en tu navegador
y podr\u00e1s jugar inmediatamente sin instalar nada. Aparecer\u00e1 un men\u00fa
inicial donde elegir entre tres modos: Jugador vs Jugador, Jugador vs M\u00e1quina
o M\u00e1quina vs M\u00e1quina. En los modos con IA se pueden seleccionar cinco
dificultades: F\u00e1cil, Medio, Avanzado, Dif\u00edcil e Imposible. El jugador 1
aparece en azul y el jugador 2 en rojo.
Para quienes prefieran Python, existe una versión con `pygame`. Instala la dependencia con `pip install pygame` y ejecuta `python main.py` desde la carpeta `python` para elegir modo y dificultad.

El campo de juego es m\u00e1s ancho para facilitar la reacci\u00f3n y, tras cada
punto, la pelota se queda dos segundos en el centro antes de ponerse en
marcha de nuevo.
La partida termina cuando un jugador alcanza cinco tantos. Aparece un mensaje con el ganador y puedes reiniciar sin recargar la página. Cada vez que la pelota toca una paleta su velocidad aumenta ligeramente y se reproducen pequeños sonidos al golpear o marcar.

## Configuraci\u00f3n en Unity

1. Abre Unity y crea un proyecto 2D nuevo o usa uno existente.
2. Copia la carpeta `Assets` de este repositorio dentro del proyecto.
3. Crea dos objetos para las paletas y as\u00edgnales el script `PaddleController`.
   La paleta izquierda (jugador 1) se muestra en azul y la derecha (jugador 2) en rojo.
4. Crea un objeto para la pelota con un `Rigidbody2D` y el script `Ball`.
5. A\u00f1ade dos objetos con colisionador en los extremos izquierdo y derecho, n\u00f3mbralos `LeftGoal` y `RightGoal` y dales la etiqueta `Goal`.
6. Crea dos elementos `UI Text` para mostrar las puntuaciones y enl\u00e1zalos en el `GameManager`.
7. Para jugar contra la m\u00e1quina activa el componente `AIPaddleController` en la pala derecha y escoge el modo **Jugador vs M\u00e1quina** en el `GameManager`.
   Si quieres ver un duelo entre IAs, a\u00f1ade tambi\u00e9n ese componente a la pala izquierda y selecciona el modo **M\u00e1quina vs M\u00e1quina**.
   En ambos casos puedes elegir la dificultad del 1 al 5.

Las paletas se mueven con `W/S` para el jugador izquierdo y `Flecha Arriba/Flecha Abajo` para el derecho. Al pulsar Play comenzar\u00e1 la partida.

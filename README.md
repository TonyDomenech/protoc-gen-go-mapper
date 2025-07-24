# proyecto-videojuego
Este es el repositorio donde se conectaran los archivos con el proyecto de Unity.

## Pong en Python

En la carpeta `pong` se incluye un peque\u00f1o juego de ping pong programado con
[Pygame](https://www.pygame.org/). El archivo `pong.py` puede ejecutarse de forma
local y tambi\u00e9n est\u00e1 preparado para mostrarse en un navegador usando
[pygbag](https://pygame-web.github.io/pygbag/).

### Ejecuci\u00f3n local
Instala las dependencias de Pygame e inicia el juego:

```bash
pip install pygame
python pong/pong.py
```

### Versi\u00f3n web
Para ver el juego en un navegador es necesario contar con `pygbag`. Tras
instalarlo se puede compilar o bien abrir directamente el archivo
`pong/index.html` junto con `pong.py`.

```bash
pip install pygbag
cd pong
pygbag --build pong.py
```

Esto genera una carpeta `build/web` con los archivos listos para ser servidos en
un servidor est\u00e1tico.

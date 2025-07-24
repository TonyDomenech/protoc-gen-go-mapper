import pygame
from ball import Ball
from paddle_controller import Paddle
from ai_paddle_controller import AIPaddle

WINDOW_WIDTH = 1000
WINDOW_HEIGHT = 600
PADDLE_WIDTH = 10
PADDLE_HEIGHT = 100
BALL_SIZE = 10
SCORE_TO_WIN = 5

class GameManager:
    """Main game loop"""
    def __init__(self):
        pygame.init()
        self.screen = pygame.display.set_mode((WINDOW_WIDTH, WINDOW_HEIGHT))
        pygame.display.set_caption('Ping Pong')
        self.clock = pygame.time.Clock()
        self.mode, self.difficulty = self.menu()
        self.ball = Ball(self.difficulty, WINDOW_WIDTH, WINDOW_HEIGHT, BALL_SIZE)
        self.left_paddle = Paddle(10, WINDOW_HEIGHT, PADDLE_WIDTH, PADDLE_HEIGHT, pygame.Color('blue'))
        self.right_paddle = Paddle(WINDOW_WIDTH-PADDLE_WIDTH-10, WINDOW_HEIGHT, PADDLE_WIDTH, PADDLE_HEIGHT, pygame.Color('red'))
        self.left_ai = AIPaddle(self.left_paddle, self.difficulty, self.ball)
        self.right_ai = AIPaddle(self.right_paddle, self.difficulty, self.ball)
        self.left_score = 0
        self.right_score = 0
        self.game_over = False
        self.font = pygame.font.SysFont(None, 36)
        self.ball.reset()

    def menu(self):
        print('Selecciona el modo de juego:')
        print('1) Jugador vs Jugador')
        print('2) Jugador vs Máquina')
        print('3) Máquina vs Máquina')
        mode = input('Opción [1-3]: ').strip()
        if mode not in ('1','2','3'):
            mode = '1'
        if mode in ('2','3'):
            diff = input('Dificultad (1-Fácil ... 5-Imposible) [3]: ').strip()
            if diff not in ('1','2','3','4','5'):
                diff = '3'
            difficulty = int(diff)
        else:
            difficulty = 3
        return mode, difficulty

    def check_winner(self):
        if self.left_score >= SCORE_TO_WIN or self.right_score >= SCORE_TO_WIN:
            self.game_over = True
            return True
        return False

    def draw(self):
        self.screen.fill((0,0,0))
        pygame.draw.rect(self.screen, self.left_paddle.color, self.left_paddle.rect)
        pygame.draw.rect(self.screen, self.right_paddle.color, self.right_paddle.rect)
        pygame.draw.rect(self.screen, (255,255,255), self.ball.rect)
        ls = self.font.render(str(self.left_score), True, pygame.Color('white'))
        rs = self.font.render(str(self.right_score), True, pygame.Color('white'))
        self.screen.blit(ls, (WINDOW_WIDTH//4, 20))
        self.screen.blit(rs, (WINDOW_WIDTH*3//4, 20))
        if self.game_over:
            text = 'Gana Azul' if self.left_score>self.right_score else 'Gana Rojo'
            wt = self.font.render(text, True, pygame.Color('yellow'))
            rect = wt.get_rect(center=(WINDOW_WIDTH//2, WINDOW_HEIGHT//2))
            self.screen.blit(wt, rect)
        pygame.display.flip()

    def run(self):
        running = True
        while running:
            for event in pygame.event.get():
                if event.type == pygame.QUIT:
                    running = False
                if event.type == pygame.USEREVENT + 1 and self.ball.waiting:
                    self.ball.launch(self.ball.next_dir)
            keys = pygame.key.get_pressed()
            if self.mode in ('1','2'):
                if keys[pygame.K_w]:
                    self.left_paddle.move(-self.left_paddle.speed)
                if keys[pygame.K_s]:
                    self.left_paddle.move(self.left_paddle.speed)
            else:
                self.left_ai.update()
            if self.mode == '1':
                if keys[pygame.K_UP]:
                    self.right_paddle.move(-self.right_paddle.speed)
                if keys[pygame.K_DOWN]:
                    self.right_paddle.move(self.right_paddle.speed)
            else:
                self.right_ai.update()
            self.ball.update()
            if self.ball.rect.top <= 0 or self.ball.rect.bottom >= WINDOW_HEIGHT:
                self.ball.bounce_y()
            if self.ball.rect.colliderect(self.left_paddle.rect) and self.ball.vx < 0:
                self.ball.rect.left = self.left_paddle.rect.right
                self.ball.bounce_x()
            if self.ball.rect.colliderect(self.right_paddle.rect) and self.ball.vx > 0:
                self.ball.rect.right = self.right_paddle.rect.left
                self.ball.bounce_x()
            if self.ball.rect.left <= 0:
                self.right_score += 1
                if not self.check_winner():
                    self.ball.reset(direction=1)
            elif self.ball.rect.right >= WINDOW_WIDTH:
                self.left_score += 1
                if not self.check_winner():
                    self.ball.reset(direction=-1)
            self.draw()
            self.clock.tick(60)
        pygame.quit()


if __name__ == "__main__":
    GameManager().run()


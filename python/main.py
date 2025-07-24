import pygame
import sys
import time

WINDOW_WIDTH = 1000
WINDOW_HEIGHT = 600
PADDLE_WIDTH = 10
PADDLE_HEIGHT = 100
BALL_SIZE = 10
SCORE_TO_WIN = 5

BALL_SPEED = {
    1: 2.0,
    2: 2.5,
    3: 3.0,
    4: 3.5,
    5: 4.0,
}
AI_SPEED = {
    1: 3.0,
    2: 4.0,
    3: 5.0,
    4: 6.0,
    5: 7.0,
}
AI_ERROR = {
    1: 60,
    2: 40,
    3: 20,
    4: 10,
    5: 0,
}

class Ball:
    def __init__(self, difficulty):
        self.base_speed = BALL_SPEED[difficulty]
        self.rect = pygame.Rect(
            WINDOW_WIDTH // 2 - BALL_SIZE // 2,
            WINDOW_HEIGHT // 2 - BALL_SIZE // 2,
            BALL_SIZE,
            BALL_SIZE,
        )
        self.vx = 0
        self.vy = 0
        self.speed = self.base_speed
        self.waiting = True

    def launch(self, direction=None):
        self.speed = self.base_speed
        direction = direction or (1 if pygame.time.get_ticks() % 2 == 0 else -1)
        self.vx = direction * self.speed
        self.vy = (pygame.random.random() * 2 - 1) * self.speed
        self.waiting = False

    def reset(self, direction=None):
        self.rect.x = WINDOW_WIDTH // 2 - BALL_SIZE // 2
        self.rect.y = WINDOW_HEIGHT // 2 - BALL_SIZE // 2
        self.vx = self.vy = 0
        self.waiting = True
        pygame.time.set_timer(pygame.USEREVENT + 1, 2000, loops=1)
        self.next_dir = direction

    def update(self):
        if self.waiting:
            return
        self.rect.x += int(self.vx)
        self.rect.y += int(self.vy)

    def bounce_y(self):
        self.vy *= -1

    def bounce_x(self):
        self.vx = -self.vx
        self.speed *= 1.05
        self.vx = (1 if self.vx > 0 else -1) * self.speed

class Paddle:
    def __init__(self, x, color):
        self.rect = pygame.Rect(x, WINDOW_HEIGHT // 2 - PADDLE_HEIGHT // 2,
                                PADDLE_WIDTH, PADDLE_HEIGHT)
        self.speed = 7
        self.color = color

    def move(self, dy):
        self.rect.y += dy
        self.rect.y = max(0, min(WINDOW_HEIGHT - PADDLE_HEIGHT, self.rect.y))

class AIPaddle:
    def __init__(self, paddle, difficulty, ball):
        self.paddle = paddle
        self.difficulty = difficulty
        self.ball = ball

    def update(self):
        target = self.ball.rect.centery + (pygame.random.random() * 2 - 1) * AI_ERROR[self.difficulty]
        mid = self.paddle.rect.centery
        if mid < target:
            self.paddle.move(AI_SPEED[self.difficulty])
        elif mid > target:
            self.paddle.move(-AI_SPEED[self.difficulty])

class GameManager:
    def __init__(self):
        pygame.init()
        self.screen = pygame.display.set_mode((WINDOW_WIDTH, WINDOW_HEIGHT))
        pygame.display.set_caption('Ping Pong')
        self.clock = pygame.time.Clock()
        self.mode, self.difficulty = self.menu()
        self.ball = Ball(self.difficulty)
        self.left_paddle = Paddle(10, pygame.Color('blue'))
        self.right_paddle = Paddle(WINDOW_WIDTH - PADDLE_WIDTH - 10, pygame.Color('red'))
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
        sys.exit()

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

if __name__ == '__main__':
    GameManager().run()

import pygame

WIDTH, HEIGHT = 800, 600
WHITE = (255, 255, 255)
BLACK = (0, 0, 0)

PADDLE_WIDTH = 15
PADDLE_HEIGHT = 90
BALL_SIZE = 20
PADDLE_SPEED = 5
BALL_SPEED_X = 5
BALL_SPEED_Y = 5


def main():
    pygame.init()
    screen = pygame.display.set_mode((WIDTH, HEIGHT))
    pygame.display.set_caption("Ping Pong")

    paddle1 = pygame.Rect(20, HEIGHT // 2 - PADDLE_HEIGHT // 2, PADDLE_WIDTH, PADDLE_HEIGHT)
    paddle2 = pygame.Rect(WIDTH - 20 - PADDLE_WIDTH, HEIGHT // 2 - PADDLE_HEIGHT // 2, PADDLE_WIDTH, PADDLE_HEIGHT)
    ball = pygame.Rect(WIDTH // 2 - BALL_SIZE // 2, HEIGHT // 2 - BALL_SIZE // 2, BALL_SIZE, BALL_SIZE)
    ball_speed = [BALL_SPEED_X, BALL_SPEED_Y]

    score1 = 0
    score2 = 0
    font = pygame.font.Font(None, 36)
    clock = pygame.time.Clock()

    running = True
    while running:
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                running = False

        keys = pygame.key.get_pressed()
        if keys[pygame.K_w] and paddle1.top > 0:
            paddle1.y -= PADDLE_SPEED
        if keys[pygame.K_s] and paddle1.bottom < HEIGHT:
            paddle1.y += PADDLE_SPEED
        if keys[pygame.K_UP] and paddle2.top > 0:
            paddle2.y -= PADDLE_SPEED
        if keys[pygame.K_DOWN] and paddle2.bottom < HEIGHT:
            paddle2.y += PADDLE_SPEED

        ball.x += ball_speed[0]
        ball.y += ball_speed[1]

        if ball.top <= 0 or ball.bottom >= HEIGHT:
            ball_speed[1] = -ball_speed[1]
        if ball.colliderect(paddle1) and ball_speed[0] < 0:
            ball_speed[0] = -ball_speed[0]
        if ball.colliderect(paddle2) and ball_speed[0] > 0:
            ball_speed[0] = -ball_speed[0]

        if ball.left <= 0:
            score2 += 1
            ball.center = (WIDTH // 2, HEIGHT // 2)
            ball_speed[0] = -BALL_SPEED_X
        if ball.right >= WIDTH:
            score1 += 1
            ball.center = (WIDTH // 2, HEIGHT // 2)
            ball_speed[0] = BALL_SPEED_X

        screen.fill(BLACK)
        pygame.draw.rect(screen, WHITE, paddle1)
        pygame.draw.rect(screen, WHITE, paddle2)
        pygame.draw.ellipse(screen, WHITE, ball)
        pygame.draw.aaline(screen, WHITE, (WIDTH // 2, 0), (WIDTH // 2, HEIGHT))

        text = font.render(f"{score1}   {score2}", True, WHITE)
        screen.blit(text, (WIDTH // 2 - text.get_width() // 2, 20))

        pygame.display.flip()
        clock.tick(60)

    pygame.quit()


if __name__ == "__main__":
    main()

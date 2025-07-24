class Ball:
    """Ball logic for ping pong game"""
    SPEED_MAP = {1: 2.0, 2: 2.5, 3: 3.0, 4: 3.5, 5: 4.0}

    def __init__(self, difficulty, window_width, window_height, size):
        import pygame
        self.win_w = window_width
        self.win_h = window_height
        self.size = size
        self.base_speed = self.SPEED_MAP[difficulty]
        self.rect = pygame.Rect(window_width // 2 - size // 2,
                                window_height // 2 - size // 2,
                                size, size)
        self.speed = self.base_speed
        self.vx = 0
        self.vy = 0
        self.waiting = True
        self.next_dir = 1

    def launch(self, direction=None):
        import pygame
        self.speed = self.base_speed
        direction = direction or (1 if pygame.time.get_ticks() % 2 == 0 else -1)
        self.vx = direction * self.speed
        self.vy = (pygame.random.random() * 2 - 1) * self.speed
        self.waiting = False

    def reset(self, direction=None):
        import pygame
        self.rect.x = self.win_w // 2 - self.size // 2
        self.rect.y = self.win_h // 2 - self.size // 2
        self.vx = self.vy = 0
        self.waiting = True
        self.next_dir = direction or self.next_dir
        pygame.time.set_timer(pygame.USEREVENT + 1, 2000, loops=1)

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

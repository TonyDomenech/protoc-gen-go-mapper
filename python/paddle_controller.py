class Paddle:
    """Player-controlled paddle"""
    def __init__(self, x, window_height, width, height, color):
        import pygame
        self.rect = pygame.Rect(x, window_height // 2 - height // 2,
                                width, height)
        self.speed = 7
        self.window_height = window_height
        self.color = color

    def move(self, dy):
        self.rect.y += dy
        self.rect.y = max(0, min(self.window_height - self.rect.height, self.rect.y))

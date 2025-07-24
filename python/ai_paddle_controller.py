class AIPaddle:
    """AI controller for a paddle"""
    SPEED = {1:3.0,2:4.0,3:5.0,4:6.0,5:7.0}
    ERROR = {1:60,2:40,3:20,4:10,5:0}

    def __init__(self, paddle, difficulty, ball):
        self.paddle = paddle
        self.difficulty = difficulty
        self.ball = ball

    def update(self):
        import pygame
        target = self.ball.rect.centery + (pygame.random.random()*2 - 1) * self.ERROR[self.difficulty]
        if self.paddle.rect.centery < target:
            self.paddle.move(self.SPEED[self.difficulty])
        elif self.paddle.rect.centery > target:
            self.paddle.move(-self.SPEED[self.difficulty])

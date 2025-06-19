const express = require('express');
const router = express.Router();
const context = require('../contexts/context');

router.get('/', async (req, res) => {
  const todos = await context.todos.findMany();
  res.json(todos);
});

router.post('/', async (req, res) => {
  const { title, completed } = req.body;

  if (!title || title.trim() === '')
    return res.status(400).json({ error: 'Title cannot be null or empty' });

  const newTodo = await context.todos.create({
    data: { Title: title, Completed: !!completed },
  });

  res.status(201).json(newTodo);
});

router.put('/:id', async (req, res) => {
  const { id } = req.params;
  const { title, completed } = req.body;
  const entity = await context.todos.findUnique({ where: { Id: id } });

  if (!entity)
    return res.status(404).send();

  const updated = await context.todos.update({
    where: { Id: id },
    data: { Title: title, Completed: completed },
  });

  res.status(200).json(updated);
});

router.delete('/:id', async (req, res) => {
  const { id } = req.params;
  const entity = await context.todos.findUnique({ where: { Id: id } });

  if (!entity)
    return res.status(404).send();

  await context.todos.delete({ where: { Id: id } });
  res.status(204).send();
});

module.exports = router;
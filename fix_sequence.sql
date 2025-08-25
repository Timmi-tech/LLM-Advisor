-- Fix the sequence for PostgraduatePrograms table
-- This will set the sequence to the next available ID

SELECT setval('public."PostgraduatePrograms_Id_seq"', 
    COALESCE((SELECT MAX("Id") FROM public."PostgraduatePrograms"), 0) + 1, 
    false);
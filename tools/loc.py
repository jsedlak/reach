import os
import re

def count_lines_of_code(directory):
    total_lines = 0
    comment_lines = 0
    using_lines = 0
    empty_lines = 0
    brace_lines = 0
    code_lines = 0

    for root, _, files in os.walk(directory):
        for file in files:
            if file.endswith('.cs'):
                file_path = os.path.join(root, file)
                with open(file_path, 'r') as f:
                    lines = f.readlines()
                    for line in lines:
                        stripped_line = line.strip()

                        if stripped_line.startswith('//'):
                            comment_lines += 1
                        elif stripped_line.startswith('using'):
                            using_lines += 1
                        elif stripped_line == '':
                            empty_lines += 1
                        elif stripped_line in ['{', '}']:
                            brace_lines += 1
                        else:
                            code_lines += 1

                    total_lines += len(lines)
                    # print(f'{file}: {len(lines)} lines')

    print(f'Total lines of code: {total_lines}')
    print(f'Comment lines: {comment_lines}')
    print(f'Using statements: {using_lines}')
    print(f'Empty lines: {empty_lines}')
    print(f'Brace lines: {brace_lines}')
    print(f'Actual code lines: {code_lines}')

# Replace 'your_directory_path' with the path to the directory you want to search
directory_path = '../'
count_lines_of_code(directory_path)

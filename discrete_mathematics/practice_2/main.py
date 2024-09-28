import numpy as np
import pandas as pd
import re
import math
from collections import Counter
from itertools import islice

def clean_text(text):
    return re.sub("[^a-zа-я]", "", text.lower())

def entropy(text):
    frequency = Counter(text)
    total_characters = len(text)
    probabilities = [freq / total_characters for freq in frequency.values()]
    return -sum(p * math.log2(p) for p in probabilities)

def average_code_length_uniform(alphabet_size):
    return math.log2(alphabet_size)

def redundancy(entropy, avg_code_length):
    return avg_code_length - entropy

def shannon_fano_coding(freq_table):
    sorted_freq = sorted(freq_table.items(), key=lambda item: item[1], reverse=True)
    return _shannon_fano_recursive(sorted_freq)

def _shannon_fano_recursive(sorted_freq):
    if len(sorted_freq) == 1:
        return {sorted_freq[0][0]: ""}
    total_sum = sum([item[1] for item in sorted_freq])
    cumulative_sum = 0
    split_index = 0
    for i, item in enumerate(sorted_freq):
        cumulative_sum += item[1]
        if cumulative_sum >= total_sum / 2:
            split_index = i + 1
            break
    left_part = _shannon_fano_recursive(sorted_freq[:split_index])
    right_part = _shannon_fano_recursive(sorted_freq[split_index:])
    left_part = {key: "0" + code for key, code in left_part.items()}
    right_part = {key: "1" + code for key, code in right_part.items()}
    left_part.update(right_part)
    return left_part

def encode_n_grams(text, code_table, n=2):
    n_grams = [''.join(islice(text, i, i + n)) for i in range(0, len(text) - n + 1, n)]
    return ''.join(code_table[gram] for gram in n_grams if gram in code_table)

def decode_n_grams(encoded_text, code_table):
    reverse_table = {v: k for k, v in code_table.items()}
    decoded_text = []
    buffer = ""
    for bit in encoded_text:
        buffer += bit
        if buffer in reverse_table:
            decoded_text.append(reverse_table[buffer])
            buffer = ""
    return ''.join(decoded_text)

def get_frequency_table(text, n=1):
    n_grams = [''.join(islice(text, i, i + n)) for i in range(len(text) - n + 1)]
    return Counter(n_grams)

def average_code_length(freq_table, code_table):
    total_symbols = sum(freq_table.values())
    return sum(len(code_table[symbol]) * freq / total_symbols for symbol, freq in freq_table.items())

def compression_efficiency(entropy, avg_code_length):
    return entropy / avg_code_length

text = "this is a sample text to be encoded using shannon fano coding method."

cleaned_text = clean_text(text)

text_entropy = entropy(cleaned_text)
alphabet_size = len(set(cleaned_text))
avg_code_length_uniform = average_code_length_uniform(alphabet_size)
text_redundancy = redundancy(text_entropy, avg_code_length_uniform)

freq_table_single = get_frequency_table(cleaned_text, n=1)
shannon_fano_single = shannon_fano_coding(freq_table_single)
encoded_single = encode_n_grams(cleaned_text, shannon_fano_single, n=1)
decoded_single = decode_n_grams(encoded_single, shannon_fano_single)
avg_code_length_single = average_code_length(freq_table_single, shannon_fano_single)
compression_efficiency_single = compression_efficiency(text_entropy, avg_code_length_single)

freq_table_double = get_frequency_table(cleaned_text, n=2)
shannon_fano_double = shannon_fano_coding(freq_table_double)
encoded_double = encode_n_grams(cleaned_text, shannon_fano_double, n=2)
decoded_double = decode_n_grams(encoded_double, shannon_fano_double)
avg_code_length_double = average_code_length(freq_table_double, shannon_fano_double)
compression_efficiency_double = compression_efficiency(text_entropy, avg_code_length_double)

results = {
    'Энтропия': text_entropy,
    'Средняя длина кода (равномерная)': avg_code_length_uniform,
    'Избыточность': text_redundancy,
    'Средняя длина кода (одиночный)': avg_code_length_single,
    'Эффективность сжатия (одиночная)': compression_efficiency_single,
    'Средняя длина кода (двойная)': avg_code_length_double,
    'Эффективность сжатия (двойная)': compression_efficiency_double,
    'Закодированный текст (одинарный)': encoded_single,
    'Расшифрованный текст (одиночный)': decoded_single,
    'Закодированный текст (двойной)': encoded_double,
    'Расшифрованный текст (двойной)': decoded_double,
}

pd.set_option('display.max_colwidth', None)
results_df = pd.DataFrame(list(results.items()), columns=['Условие', 'Значение'])
import ace_tools_open as tools; tools.display_dataframe_to_user(name="Результат выполнения программы", dataframe=results_df)
